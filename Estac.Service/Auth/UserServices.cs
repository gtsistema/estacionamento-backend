using AutoMapper;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Motorista;
using Estac.Infra.Context;
using Estac.Infra.Repositories;
using Estac.Service.Extensions;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Estac.Service.Auth
{
    public class UserServices : ServiceResult<ApplicationUser>, IUserServices
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignManager _signManager;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly GtsContext _context;
        private readonly BearerTokenSettings _bearerTokenSettings;
        private readonly UserManager<ApplicationUser> _identityUserManager;
        private readonly IPessoaRepositories _pessoaRepositories;
        private readonly IPerfilRepositories _perfilRepositories;
        private readonly IMenuRepositories _menuRepositories;

        public UserServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GtsContext context,
               IErrorServices _errorApplication,
               UserManager<ApplicationUser> _identityUserManager,
               IPessoaRepositories _pessoaRepositories,
               IPerfilRepositories _perfilRepositories,
               IMenuRepositories _menuRepositories) : base(_errorApplication)
        {
            _bearerTokenSettings = bearerTokenSettings.Value;
            _userManager = userManager;
            _signManager = signManager;
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
            this._identityUserManager = _identityUserManager;
            this._pessoaRepositories =  _pessoaRepositories;
            this._perfilRepositories = _perfilRepositories;
            this._menuRepositories = _menuRepositories;
        }

        public async Task<ActionResult> LoginAsync(LoginInput dto)
        {
            try
            {
                var result = await _signManager.PasswordSignInAsync(dto.UserName, dto.Password);

                if (!result.Succeeded)
                    return await RetornNo(false, Resources.Resources.MSG_Usuario_Ou_Senha_Invalida);

                var user = await _identityUserManager.FindByNameAsync(dto.UserName);

                return await RetornOk(await MontarLoginResponseAsync(user), Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch(Exception ex)
            {
                return await RetornNo(ex, ex.Message);
            }
            
        }

        public async Task<ActionResult> RegisterAsync(RegisterInput dto)
        {
            var pessoa = _mapper.Map<Pessoa>(dto.Pessoa);
            pessoa.Email = dto.Email;
            pessoa.Ativo = true;

            try
            {
                pessoa.AdicionarPapel(TipoPapel.Funcionario);

                await _pessoaRepositories.Gravar(pessoa);
                var user = _mapper.Map<ApplicationUser>(dto);
                user.PessoaId = pessoa.Id;
                user.EmailConfirmed = true;
                user.FullName = pessoa.NomeFantasia;

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return await RetornNo(false, result.Errors[0]);

                await _userManager.AddToRoleAsync(user, dto.Perfil.Name);

                return await RetornOk(Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch (Exception ex)
            {
                if(pessoa.Id > 0)
                {
                    await _pessoaRepositories.Excluir(pessoa.Id);
                }
                
                return await RetornNo(ex, Resources.Resources.MSG_OperacaoComErro);
            }
        }

        private async Task<UsuarioAcessoOutput> MontarLoginResponseAsync(ApplicationUser user)
        {
            var permissoes = await _perfilRepositories.BuscarPerfilPorUsuarioToken(user.Id);

            //var menus = await _menuRepositories.BuscarPerfilPorUsuarioToken(user.Id);

            var jwt = await GenerateJwt(user, permissoes);

            return new UsuarioAcessoOutput
            {
                Jwt = jwt,
                Menus = new List<MenuOuput>()
            };
        }

        private async Task<TokenResponse> GenerateJwt(ApplicationUser user, UsuarioRoleOuput acessoOutput)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerTokenSettings.Secret);
            var timeInMiliseconds = _bearerTokenSettings.ExpirationInMinutes * 60 * 1000;
            var expires = DateTime.UtcNow.AddMilliseconds(timeInMiliseconds);

            var refreshTimeInMiliseconds = _bearerTokenSettings.RefreshExpirationInMinutes * 60 * 1000;
            var refreshExpires = DateTime.UtcNow.AddMinutes(_bearerTokenSettings.RefreshExpirationInMinutes);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _bearerTokenSettings.Issuer,
                Audience = _bearerTokenSettings.ValidOn,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(await Permissoes(user.Id))
            });

            var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            return new TokenResponse
            {
                ExpiresIn = expires,
                TimeInMiliseconds = timeInMiliseconds,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresIn = refreshExpires,
                    TimeInMiliseconds = refreshTimeInMiliseconds
                }
            };
        }

        private async Task<List<Claim>> Permissoes(int usuarioId)
        {
            var acessoOutput = await _perfilRepositories.BuscarPerfilPorUsuarioToken(usuarioId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, acessoOutput.UserId.ToString()),
                new Claim(ClaimTypes.Name, acessoOutput.UserName),
                new Claim(ClaimTypes.Email, acessoOutput.Email ?? ""),
                new Claim(ClaimTypes.Role, acessoOutput.Role),
            };

            foreach (var permissao in acessoOutput.Permissions.Select(p => p.Descricao).Distinct())
            {
                if(permissao is not null)
                    claims.Add(new Claim("permission", permissao));
            }

            return claims;
        }
    }
}
