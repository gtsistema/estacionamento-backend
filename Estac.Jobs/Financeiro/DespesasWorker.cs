using Estac.Jobs.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estac.Jobs.Financeiro
{
    public class DespesasWorker : BackgroundService
    {
        private readonly ILogger<DespesasWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly string _config = "MovimentacaoFinanceiro";
        private GetSection _getSection;
        private ConfigJobs _configJobs;

        public DespesasWorker(ILogger<DespesasWorker> logger,
                                IServiceProvider serviceProvider,
                                IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _getSection = new GetSection(_configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    try
            //    {
            //        ConfigurationJobs();

            //        if (_configJobs.ExecutaJob)
            //        {
            //            Log.Information("{Name} - Iniciando o processo...", "MovimentacaoFinanceiroWorker");

            //            using (IServiceScope scope = _serviceProvider.CreateScope())
            //            {
            //                IMovimentacaoFinanceiroRepositories _repositories =
            //                scope.ServiceProvider.GetRequiredService<IMovimentacaoFinanceiroRepositories>();

            //                await _repositories.IntegracaoMovimentacaoProtheusAsync();
            //            }

            //            Log.Information("{Name} - finalizou o processo.", "MovimentacaoFinanceiroWorker");
            //        }

            //        await Task.Delay(_configJobs.TimeExec, stoppingToken);
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Error("{Name} - " + $"[ERROR] - MovimentacaoFinanceiroWorker: {e.Message}", "MovimentacaoFinanceiroWorker");
            //    }
            //}
        }

        protected void ConfigurationJobs()
        {
            _configJobs = new ConfigJobs();

            _configJobs.IntervaloExecucao = _getSection.Get(_config, "Time") != null
                                    ? Convert.ToInt32(_getSection.Get(_config, "Time"))
                                    : 10;

            _configJobs.TimeExec = TimeSpan.FromMinutes(_configJobs.IntervaloExecucao);

            if (_getSection.Get(_config, "Exec").ToLower() == "true")
                _configJobs.ExecutaJob = true;
        }
    }
}
