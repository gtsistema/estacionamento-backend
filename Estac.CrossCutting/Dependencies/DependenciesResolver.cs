using Estac.Domain.Clock;
using Estac.Domain.Extensions.Notifier;
using Estac.Domain.Auth;
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
using Estac.Service.Email;
using Estac.Service.Identity;
using Estac.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Estac.Service.Movimento;
using Estac.Domain.Integration.Workers;
using Estac.Domain.Interface.Integration;
using Estac.Infra.Integration.Workers;
using Microsoft.Extensions.Options;

namespace Estac.CrossCutting.Dependencies
{
    public static class DependenciesResolver
    {
        public static IServiceCollection ResolveInjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
            services.Configure<EstacionamentoWorkersOptions>(configuration.GetSection(EstacionamentoWorkersOptions.SectionName));
            services.AddHttpClient<IEstacionamentoWorkersClient, EstacionamentoWorkersClient>()
                .ConfigureHttpClient((sp, client) =>
                {
                    var opt = sp.GetRequiredService<IOptions<EstacionamentoWorkersOptions>>().Value;
                    var baseUrl = (opt.BaseUrl ?? string.Empty).TrimEnd('/');
                    if (!string.IsNullOrEmpty(baseUrl))
                        client.BaseAddress = new Uri(baseUrl + "/");
                    var seconds = opt.TimeoutSeconds < 5 ? 5 : (opt.TimeoutSeconds > 120 ? 120 : opt.TimeoutSeconds);
                    client.Timeout = TimeSpan.FromSeconds(seconds);
                });
            services.AddScoped<IEmailSenderService, SmtpEmailSenderService>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IErrorServices, ErrorServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPerfilServices, PerfilServices>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddScoped<IVeiculoModeloService, VeiculoModeloService>();
            services.AddScoped<IMenuServices, MenuServices>();
            services.AddScoped<ITransportadoraService, TransportadoraService>();
            services.AddScoped<IEstacionamentoService, EstacionamentoService>();
            services.AddScoped<IEntradaSaidaService, EntradaSaidaService>();
            services.AddScoped<IConfiguracaoCobrancaService, ConfiguracaoCobrancaService>();
            services.AddScoped<IFaturaService, FaturaService>();

            // repositories
            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddScoped(typeof(IBaseRepositoriesNone<>), typeof(BaseRepositoriesNone<>));
            services.AddScoped(typeof(IBaseRepositoriesIdentityNone<>), typeof(BaseRepositoriesIdentityNone<>));

            services.AddScoped<IVeiculoRepositories, VeiculoRepositories>();
            services.AddScoped<IVeiculoMotoristaRepositories, VeiculoMotoristaRepositories>();
            services.AddScoped<IMotoristaRepositories, MotoristaRepositories>();
            services.AddScoped<IVeiculoModeloRepositories, VeiculoModeloRepositories>();
            services.AddScoped<IMenuRepositories, MenuRepositories>();
            services.AddScoped<ITransportadoraRepositories, TransportadoraRepositories>();
            services.AddScoped<IPessoaContatoRepositories, PessoaContatoRepositories>();
            services.AddScoped<IPessoaEnderecoRepositories, PessoaEnderecoRepositories>();
            services.AddScoped<IPessoaRepositories, PessoaRepositories>();
            services.AddScoped<IPerfilRepositories, PerfilRepositories>();
            services.AddScoped<IUsuarioRepositories, UsuarioRepositories>();
            services.AddScoped<IEstacionamentoRepositories, EstacionamentoRepositories>();
            services.AddScoped<IEntradaSaidaRepositories, EntradaSaidaRepositories>();
            services.AddScoped<IConfiguracaoCobrancaRepositories, ConfiguracaoCobrancaRepositories>();
            services.AddScoped<IFaturaRepositories, FaturaRepositories>();

            return services;
        }
    }
}