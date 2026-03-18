using Microsoft.AspNetCore.Identity;
using System.Text;
using System;
using Estac.Domain.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Estac.Infra.Context;
using Estac.Domain.Extensions;
using Estac.Service.Identity.Interface;
using Estac.Service.Identity;
using Estac.Domain.Models.Auth;

namespace Estac.Api.Extensions
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
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

                if (string.IsNullOrEmpty(bearerTokenSettings?.Secret))
                    throw new Exception("JWT Secret não configurado");

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
            catch (FormatException)
            {
                throw;
            }

            // register application managers for DI
            services.AddScoped<IApplicationSignManager, ApplicationSignManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();

            return services;
        }
    }
}
