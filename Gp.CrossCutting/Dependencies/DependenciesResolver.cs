using Gp.Domain.Auth;
using Gp.Domain.Clock;
using Gp.Domain.Extensions.Notifier;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Repositories.Cursos;
using Gp.Domain.Interface.Services;
using Gp.Domain.Interface.Services.Auth;
using Gp.Domain.Interface.Services.Cursos;
using Gp.Domain.Output;
using Gp.Infra.Repositories;
using Gp.Infra.Repositories.Cursos;
using Gp.Infra.Repository;
using Gp.Service;
using Gp.Service.Auth;
using Gp.Service.Cursos;
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
            services.AddScoped<IErrorServices, ErrorServices>();

            //services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IDespesaServices, DespesaServices>();
            services.AddScoped<IReceitaServices, ReceitaServices>();
            services.AddScoped<IOrcamentoServices, OrcamentoServices>();


            // repositories
            services.AddScoped<IDespesaRepositories, DespesaRepositories>();
            services.AddScoped<IReceitaRepositories, ReceitaRepositories>();
            services.AddScoped<IOrcamentoRepositories, OrcamentoRepositories>();
            services.AddScoped<IDespesaLancamentoRepositories, DespesaLancamentoRepositories>();
            services.AddScoped<IReceitaLancamentoRepositories, ReceitaLancamentoRepositories>();


            // cursos
            services.AddScoped<ILivroServices, LivroServices>();
            services.AddScoped<ILivroRepositories, LivroRepositories>();

            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));

            
            return services;
        }
    }
}
