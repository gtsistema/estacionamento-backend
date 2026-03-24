using AutoMapper;
using Estac.Api.Controllers.Base;
using Estac.Api.Controllers.Base.Claim;
using Estac.Api.Extensions;
using Estac.CrossCutting.Dependencies;
using Estac.Domain.Mappers;
using Estac.Domain.Mappers.Auth;
using Estac.Infra.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Estac.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = env;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get   ; }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                ResolveMiniProfile(services);

                services.AddHealthChecks();

                services.AddCors();

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.ResolveData(Configuration)
                        .ResolveInjectDependencies(Configuration)
                        .AddIdentityConfiguration(Configuration); // 🔐 JWT já configurado aqui

                services.AddDistributedMemoryCache();

                // ✅ NÃO adicionar AddAuthentication novamente (evita erro Bearer duplicado)

                // ✅ Customização do JWT (401 e 403)
                services.PostConfigure<JwtBearerOptions>("Bearer", options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var result = System.Text.Json.JsonSerializer.Serialize(new
                            {
                                erro = "Token inválido ou não informado."
                            });

                            return context.Response.WriteAsync(result);
                        },

                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var result = System.Text.Json.JsonSerializer.Serialize(new
                            {
                                erro = "Você não tem permissão para acessar este endpoint."
                            });

                            return context.Response.WriteAsync(result);
                        }
                    };
                });

                services.AddAuthorization();

                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                services.AddControllers()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "GTS API",
                        Version = "v1"
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Insira o token JWT desta forma: Bearer {seu token}"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
                });

                // 🔐 Permissões customizadas
                services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
                services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ResolveMiniProfile(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<VeiculoProfile>();
                cfg.AddProfile<MotoristaProfile>();
                cfg.AddProfile<EstacionamentoProfile>();
                cfg.AddProfile<PessoaProfile>();
                cfg.AddProfile<MenuProfile>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GtsContext context,
           ILoggerFactory loggerFactory, IHttpContextAccessor httpContext)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meu API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/index.html");
                    return Task.CompletedTask;
                });
            });

            var automaticRunDbMigrations = Configuration.GetValue<bool>("AutomaticRunDbMigrations");
            if (automaticRunDbMigrations)
            {
                context.Database.Migrate();
            }
        }
    }
}
