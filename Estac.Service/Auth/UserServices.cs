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
using System.Text;

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

        public UserServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GtsContext context,
               IErrorServices _errorApplication,
               UserManager<ApplicationUser> _identityUserManager,
               IPessoaRepositories _pessoaRepositories,
               IPerfilRepositories _perfilRepositories) : base(_errorApplication)
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

            try
            {
                pessoa.AdicionarPapel(TipoPapel.Funcionario);

                await _pessoaRepositories.Gravar(pessoa);
                var user = _mapper.Map<ApplicationUser>(dto);
                user.PessoaId = pessoa.Id;

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
            var response = await _perfilRepositories.BuscarPerfilPermissaoUsuario(user.Id);

            response.Jwt = GenerateJwt(user);
            return response;
        }

        private TokenResponse GenerateJwt(ApplicationUser user)
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
    }
}
