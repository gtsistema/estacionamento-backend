using Estac.Infra.Context;
using Estac.Service.Seed.Gts;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;

namespace Estac.Service.Seed
{
    public class MigrationSeed
    {
        public static async Task MigrationSeedAsync(IServiceProvider services)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var identityContext = services.GetRequiredService<IdentityContext>();
            var gtsContext = services.GetRequiredService<GtsContext>();

            try
            {
                 await new GtsBase().ExecutarAsync(services, gtsContext);
                 await new IdentityBase().ExecuteAsync(services, identityContext);

                 transaction.Complete();
            }
            catch (Exception ex)
            {
                transaction.Dispose();

                throw new Exception("Erro ao executar o seed de migração.", ex);
            }
        }
    }
}