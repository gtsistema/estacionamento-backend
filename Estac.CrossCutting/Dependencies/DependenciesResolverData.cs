using Estac.CrossCutting.Helpers;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Estac.CrossCutting.Dependencies
{
    public static class DependenciesResolverData
    {
        public static IServiceCollection ResolveData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            LogConexao(services, connectionString);

            services.AddDbContext<GtsContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString));

            services.Configure<ConnectionStringsConfig>(
                configuration.GetSection("ConnectionStrings"));

            services.AddScoped<IDapperRepositories, DapperRepositories>();



            return services;
        }

        private static void LogConexao(IServiceCollection services, string connectionString)
        {
            // LOG CORRETO
            Console.WriteLine($"🔥 ConnectionString: {connectionString}");

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("🔥 Connection: {conn}", connectionString);

        }
    }
}
