using Estac.Domain.Auth;
using Estac.Domain.Clock;
using Estac.Domain.Extensions.Notifier;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Cursos;
using Estac.Domain.Interface.Services;
using Estac.Domain.Interface.Services.Auth;
using Estac.Domain.Interface.Services.Cursos;
using Estac.Domain.Output;
using Estac.Infra.Repositories;
using Estac.Infra.Repositories.Cursos;
using Estac.Infra.Repository;
using Estac.Service;
using Estac.Service.Auth;
using Estac.Service.Cursos;
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
            services.AddScoped<IDespesaServices, DespesaServices>();
            services.AddScoped<IReceitaServices, ReceitaServices>();
            services.AddScoped<IOrcamentoServices, OrcamentoServices>();


            // repositories
            services.AddScoped<IDespesaRepositories, DespesaRepositories>();
            services.AddScoped<IReceitaRepositories, ReceitaRepositories>();
            services.AddScoped<IOrcamentoRepositories, OrcamentoRepositories>();
            services.AddScoped<IDespesaLancamentoRepositories, DespesaLancamentoRepositories>();
            services.AddScoped<IDespesaPagamentoRepositories, DespesaPagamentoRepositories>();
            services.AddScoped<IReceitaLancamentoRepositories, ReceitaLancamentoRepositories>();


            // cursos
            services.AddScoped<ILivroServices, LivroServices>();
            services.AddScoped<ILivroRepositories, LivroRepositories>();

            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));

            
            return services;
        }
    }
}
