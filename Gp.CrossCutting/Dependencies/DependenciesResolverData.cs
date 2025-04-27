using Gp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gp.CrossCutting.Dependencies
{
    public static class DependenciesResolverData
    {
        public static IServiceCollection ResolveData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GpContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IDapperRepositories, DapperRepositories>();


            return services;
        }
    }
}
