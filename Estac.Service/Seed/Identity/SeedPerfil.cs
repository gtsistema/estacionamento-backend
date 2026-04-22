using Estac.Domain.Models.Auth;
using Estac.Infra.Context;

namespace Estac.Service.Seed.Identity
{
    public class SeedPerfil
    {
        public async Task ExecuteAsync(IServiceProvider services, IdentityContext context)
        {
           await Gravar(services, context);
        }

        private async Task Gravar(IServiceProvider services, IdentityContext context)
        {
            await CriarPerfilAdmin(context);
            await CriarPerfilGerente(context);
        }

        private static async Task CriarPerfilAdmin(IdentityContext context)
        {
            if (context.Roles.Any(x => x.Name == "Admin"))
                return;

            var role = new ApplicationRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            context.Roles.Add(role);
            await context.SaveChangesAsync();
        }

        private static async Task CriarPerfilGerente(IdentityContext context)
        {
            if (context.Roles.Any(x => x.Name == "Gerente"))
                return;

            var role = new ApplicationRole
            {
                Name = "Gerente",
                NormalizedName = "GERENTE"
            };

            context.Roles.Add(role);
            await context.SaveChangesAsync();
        }
    }
}
