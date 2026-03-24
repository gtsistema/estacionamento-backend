using Estac.Domain.Clock;
using Estac.Domain.Extensions.Notifier;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Services;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Infra.Repositories;
using Estac.Infra.Repositories.Auth;
using Estac.Infra.Repository;
using Estac.Service;
using Estac.Service.Auth;
using Estac.Service.Identity;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;
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
            services.AddScoped<IMenuServices, MenuServices>();
            services.AddScoped<ITransportadoraService, TransportadoraService>();
            services.AddScoped<IEstacionamentoService, EstacionamentoService>();

            // repositories
            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddScoped<IVeiculoRepositories, VeiculoRepositories>();
            services.AddScoped<IMotoristaRepositories, MotoristaRepositories>();
            services.AddScoped<IVeiculoModeloRepositories, VeiculoModeloRepositories>();
            services.AddScoped<IMenuRepositories, MenuRepositories>();
            services.AddScoped<ITransportadoraRepositories, TransportadoraRepositories>();
            services.AddScoped<IPessoaRepositories, PessoaRepositories>();
            services.AddScoped<IPerfilRepositories, PerfilRepositories>();
            services.AddScoped<IEstacionamentoRepositories, EstacionamentoRepositories>();

            return services;
        }
    }
}