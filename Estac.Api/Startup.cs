using AutoMapper;
using Estac.Api.Extensions;
using Estac.CrossCutting.Dependencies;
using Estac.Domain.Mappers;
using Estac.Domain.Mappers.Auth;
using Estac.Infra.Context;
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
                services.AddCors()
                        .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                        .ResolveData(Configuration)
                        .ResolveInjectDependencies(Configuration)
                        .AddIdentityConfiguration(Configuration);

                services.AddDistributedMemoryCache();

                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Estacionamento API", Version = "v1" });

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
            catch (Exception)
            {
                throw;
            }

        }
        private void ResolveMiniProfile(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DespesaProfile>();
                cfg.AddProfile<ReceitaProfile>();
                cfg.AddProfile<OrcamentoProfile>();
                cfg.AddProfile<UsuarioProfile>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GpContext context,
           ILoggerFactory loggerFactory, IHttpContextAccessor httpContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("API ONLINE");
                });

                endpoints.MapControllers();
            });
        }
    }
}
