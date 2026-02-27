using AutoMapper;
using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models;
using Estac.Domain.Output;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Motorista;
using Estac.Infra.Context;
using Estac.Service.Extensions;
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

        public UserServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GtsContext context,
               IErrorServices _errorApplication,
               UserManager<ApplicationUser> _identityUserManager
               ) : base(_errorApplication)
        {
            _bearerTokenSettings = bearerTokenSettings.Value;
            _userManager = userManager;
            _signManager = signManager;
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
            this._identityUserManager = _identityUserManager;
        }

        public async Task<ActionResult> LoginAsync(LoginInput dto)
        {
            var result = await _signManager.PasswordSignInAsync(dto.UserName, dto.Password);

            if (!result.Succeeded)
                return await RetornNo(null, Resources.Resources.MSG_Usuario_Ou_Senha_Invalida);

            var user = await _userManager.FindByEmailAsync(dto.UserName);

            return await RetornOk(await MontarLoginResponseAsync(user), Resources.Resources.MSG_OperacaoRealizadaSucesso);
        }

        public async Task<ActionResult> RegisterAsync(RegisterInput dto)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(dto);

                //var user = new ApplicationUser { UserName = dto.UserName, Email = dto.UserName };
                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return await RetornNo(result.Errors[0], Resources.Resources.MSG_OperacaoComErro);

                return await RetornOk(Resources.Resources.MSG_OperacaoRealizadaSucesso);
            }
            catch (Exception ex)
            {
                return await RetornNo(ex.Message, Resources.Resources.MSG_OperacaoComErro, statusCode: 500);
            }
        }

        private async Task<LoginOutput> MontarLoginResponseAsync(ApplicationUser user)
        {
            var response = _mapper.Map<LoginOutput>(user);

            var roles = await _userManager.GetRolesAsync(user);
            response.Roles = roles;
            response.Jwt = GenerateJwt(user, roles);
            return response;
        }

        private TokenResponse GenerateJwt(ApplicationUser user, IEnumerable<string> roles)
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
