using Estac.Infra.Context;
using Estac.Service.Seed.Identity;

namespace Estac.Service.Seed.Gts
{
    public class IdentityBase
    {
        public async Task ExecuteAsync(IServiceProvider services, IdentityContext context)
        {
            await AllSeedAsync(services, context);
        }

        private async Task AllSeedAsync(IServiceProvider services, IdentityContext context)
        {
            await new SeedPerfil().ExecuteAsync(services, context);
            await new SeedUsuario().ExecuteAsync(services, context);
            await new SeedMenu().ExecuteAsync(services, context);
            await new SeedPerfilMenu().ExecuteAsync(services, context);

        }
    }
}