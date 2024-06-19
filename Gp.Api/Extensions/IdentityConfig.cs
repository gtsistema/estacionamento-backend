using Microsoft.AspNetCore.Identity;
using System.Text;
using System;
using Gp.Domain.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Gp.Infra.Context;
using Gp.Domain.Extensions;

namespace Gp.Api.Extensions
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<GpContext>()
                .AddErrorDescriber<IdentityPortugueseMessages>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            var bearerTokenSection = configuration.GetSection("BearerTokenSettings");
            services.Configure<BearerTokenSettings>(bearerTokenSection);

            var bearerTokenSettings = bearerTokenSection.Get<BearerTokenSettings>();
            try
            {
                var key = Encoding.ASCII.GetBytes(bearerTokenSettings.Secret);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = bearerTokenSettings.ValidOn,
                        ValidIssuer = bearerTokenSettings.Issuer
                    };
                });
            }
            catch (FormatException ex)
            {
                throw;
            }

            services.AddScoped<IApplicationSignManager, ApplicationSignManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();

            return services;
        }
    }
}
