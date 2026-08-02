using Estac.Infra.Context;

namespace Estac.Service.Seed.Gts
{
    public class GtsBase
    {
        public async Task ExecutarAsync(IServiceProvider services, GtsContext context)
        {
            await InvocAsync(services, context);
        }

        private async Task InvocAsync(IServiceProvider services, GtsContext context)
        {
            await new SeedEstacionamento().SeedAsync(services, context);

        }
    }
}
