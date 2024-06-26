using AutoMapper;
using Gp.Domain.Auth;
using Gp.Domain.Input.Auth;
using Gp.Domain.Interface.Services.Auth;
using Gp.Domain.Output;
using Gp.Domain.Output.Auth;
using Gp.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Gp.Service.Auth
{
    public class UserServices : IUserServices
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignManager _signManager;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly GpContext _context;
        private readonly BearerTokenSettings _bearerTokenSettings;
        private readonly UserManager<ApplicationUser> _identityUserManager;

        public UserServices(IApplicationUserManager userManager,
               IApplicationSignManager signManager, ICurrentUser currentUser,
               IOptions<BearerTokenSettings> bearerTokenSettings,
               IMapper mapper,
               GpContext context)
        {
            _bearerTokenSettings = bearerTokenSettings.Value;
            _userManager = userManager;
            _signManager = signManager;
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServicesResult> LoginAsync(LoginInput dto)
        {
            var result = await _signManager.PasswordSignInAsync(dto.UserName, dto.Password);

            if (!result.Succeeded)
            {
                return new ServicesResult("Login", "Usuário ou senha invalido.");
            }

            var user = await _userManager.FindByEmailAsync(dto.UserName);

            return new ServicesResult(await MontarLoginResponseAsync(user));
        }

        public async Task<ServicesResult> RegisterAsync(RegisterInput dto)
        {
            try
            {
                var user = new ApplicationUser { UserName = dto.UserName, Email = dto.UserName };
                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    return new ServicesResult("Register", result.Errors[0]);
                }

                return new ServicesResult("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return new ServicesResult("exception",ex.Message);
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
