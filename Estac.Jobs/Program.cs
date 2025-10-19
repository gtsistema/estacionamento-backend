using Estac.Jobs.Financeiro;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    IHost host = Host.CreateDefaultBuilder(args)
          .ConfigureServices(services =>
          {
              //services.AddDbContext<IntegracaoFibraContext>(
              //            options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

              //services.AddHostedService<TituloFinanceiroWorker>();
              //services.AddHostedService<MovimentacaoFinanceiroWorker>();
              //services.AddHostedService<AtendimentoPedidoWorker>();
              //services.AddHostedService<FaturaPedidoWorker>();

              //services.AddScoped<ITituloFinanceiroRepositories, TituloFinanceiroRepositories>();
              //services.AddScoped<IMovimentacaoFinanceiroRepositories, MovimentacaoFinanceiroRepositories>();
              //services.AddScoped<IAtendimentoPedidoRepositories, AtendimentoPedidoRepositories>();
              //services.AddScoped<IFaturaPedidoRepositories, FaturaPedidoRepositories>();


              //var config = new AutoMapper.MapperConfiguration(cfg =>
              //{
              //    cfg.AddProfile(new ClienteProtheusMapping());
              //    cfg.AddProfile(new TituloFinanceiroProtheusMapping());
              //    cfg.AddProfile(new MovimentacaoFinanceiroMapping());
              //});

              //IMapper mapper = config.CreateMapper();

              //services.AddSingleton(mapper);
          })
          //.UseWindowsService()
          .ConfigureAppConfiguration((context, config) =>
          {
              IConfiguration _config = config.Build();

              //Log.Logger = new LoggerConfiguration()
              //.WriteTo.Map("Name", "Other", (name, wt) => wt.File("c:/Temp" + $"/{name}/log-{name}.txt", rollingInterval: RollingInterval.Day))
              //.CreateLogger();

              //Log.Information("{Name} - Serviços inicializados...", "System");
          })
          .Build();

    try
    {
        await host.RunAsync();
}
    catch (Exception e)
    {
//        Log.Error("{Name} - Falha inesperada durante a execução: " + e.Message, "System");
//Log.Information("{Name} - Serviço parado, verifique o erro e reinicie o serviço", "System");
//    }
//    finally
//    {
//    Log.Information("{Name} - Serviço finalizado...", "System");
}
