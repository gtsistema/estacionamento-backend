using Gp.Domain.Auth;
using Gp.Domain.Clock;
using Gp.Domain.Extensions.Notifier;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Interface.Services.Auth;
using Gp.Infra.Repositories;
using Gp.Infra.Repository;
using Gp.Service;
using Gp.Service.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gp.CrossCutting.Dependencies
{
    public static class DependenciesResolver
    {
        public static IServiceCollection ResolveInjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ICurrentUser, CurrentUser>();


            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IDespesaServices, DespesaServices>();
            services.AddScoped<IReceitaServices, ReceitaServices>();
            services.AddScoped<IOrcamentoServices, OrcamentoServices>();

            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddScoped<IDespesaRepositories, DespesaRepositories>();
            services.AddScoped<IReceitaRepositories, ReceitaRepositories>();
            services.AddScoped<IOrcamentoRepositories, OrcamentoRepositories>();

            return services;
        }
    }
}
