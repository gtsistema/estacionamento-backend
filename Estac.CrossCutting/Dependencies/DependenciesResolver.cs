using Estac.Domain.Auth;
using Estac.Domain.Clock;
using Estac.Domain.Extensions.Notifier;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Output;
using Estac.Infra.Repositories;
using Estac.Infra.Repository;
using Estac.Service;
using Estac.Service.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estac.CrossCutting.Dependencies
{
    public static class DependenciesResolver
    {
        public static IServiceCollection ResolveInjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IErrorServices, ErrorServices>();

            //services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPerfilServices, PerfilServices>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddScoped<IVeiculoModeloService, VeiculoModeloService>();
            services.AddScoped<IEstacionamentoService, EstacionamentoService>();
            services.AddScoped<ITransportadoraService, TransportadoraService>();

            services.AddScoped<IApplicationSignManager, ApplicationSignManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();

            // repositories
            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddScoped<IVeiculoRepositories, VeiculoRepositories>();
            services.AddScoped<IMotoristaRepositories, MotoristaRepositories>();
            services.AddScoped<IVeiculoModeloRepositories, VeiculoModeloRepositories>();
            services.AddScoped<IEstacionamentoRepositories, EstacionamentoRepositories>();
            services.AddScoped<ITransportadoraRepositories, TransportadoraRepositories>();

            return services;
        }
    }
}