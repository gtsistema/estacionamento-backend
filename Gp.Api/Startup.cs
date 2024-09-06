using AutoMapper;
using Gp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Gp.Domain.Mappers;
using Microsoft.OpenApi.Models;
using Gp.CrossCutting.Dependencies;
using Gp.Api.Extensions;
using Gp.Domain.Mappers.Auth;

namespace Gp.Api
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
            ResolveMiniProfile(services);

            services.AddHealthChecks();
            services.AddCors()
                    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                    .ResolveData(Configuration)
                    .ResolveInjectDependencies(Configuration)
                    .AddIdentityConfiguration(Configuration);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gerenciamento Pessoal", Version = "v1" });

                // Adicionando configuração para autenticação JWT
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
        }
        private void ResolveMiniProfile(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DespesaProfile());
                cfg.AddProfile(new ReceitaProfile());
                cfg.AddProfile(new OrcamentoProfile());
                cfg.AddProfile(new UsuarioProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GpContext context,
           ILoggerFactory loggerFactory, IHttpContextAccessor httpContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meu API V1");
                    c.RoutePrefix = "swagger"; // para acessar diretamente em '/'
                });
            }

            app.UseRouting();

            app.UseSession();

            app.UseCors(options =>
                      options.AllowAnyOrigin()
                       .AllowAnyMethod().AllowAnyHeader()
             );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var automaticRunDbMigrations = Configuration.GetValue<bool>("AutomaticRunDbMigrations");
            if (automaticRunDbMigrations)
            {
                context.Database.Migrate();
            }

        }
    }
}
