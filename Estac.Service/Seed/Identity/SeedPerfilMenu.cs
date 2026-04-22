using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models.Auth;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Estac.Service.Seed.Identity
{
    public class SeedPerfilMenu
    {
        public async Task ExecuteAsync(IServiceProvider services, IdentityContext context)
        {
            await Gravar(services, context);
        }

        private async Task Gravar(IServiceProvider services, IdentityContext context)
        {
            var repositories = services.GetRequiredService<IMenuRepositories>();

            await VincularPermissoesAoPerfilGerente(context, repositories);
            await VincularPermissoesAoPerfilAdmin(context, repositories);
        }

        private static async Task VincularPermissoesAoPerfilGerente(IdentityContext context, IMenuRepositories repositories)
        {
            var perfilAdm = await context.Roles.Include(x => x.RolePermissions).FirstOrDefaultAsync(x => x.Name.ToString().ToLower() == "Gerente");

            if (perfilAdm.RolePermissions is not null && perfilAdm.RolePermissions.Any())
                return;

            var menus = await repositories.Buscar();
            var permissoesRoles = CriarRolePermissoes(perfilAdm, menus);

            await context.RolePermission.AddRangeAsync(permissoesRoles);
            await context.SaveChangesAsync();
        }

        private static async Task VincularPermissoesAoPerfilAdmin(IdentityContext context, IMenuRepositories repositories)
        {
            var perfilAdm = await context.Roles.Include(x => x.RolePermissions).FirstOrDefaultAsync(x => x.Name.ToString().ToLower() == "Admin");

            if (perfilAdm.RolePermissions is not null && perfilAdm.RolePermissions.Any())
                return;

            var menus = await repositories.Buscar();
            var permissoesRoles = CriarRolePermissoes(perfilAdm, menus);

            await context.RolePermission.AddRangeAsync(permissoesRoles);
            await context.SaveChangesAsync();
        }
        private static List<RolePermission> CriarRolePermissoes( ApplicationRole perfilAdm, List<Module> menus)
        {
            var permissoesRoles = new List<RolePermission>();

            foreach (var menu in menus)
            {
                if (menu.SubModules == null || !menu.SubModules.Any())
                {
                    permissoesRoles.Add(new RolePermission
                    {
                        ModuleId = menu.Id,
                        RoleId = perfilAdm.Id,
                    });

                    continue;
                }

                foreach (var subMenu in menu.SubModules)
                {
                    foreach (var permissao in subMenu.Permissions)
                    {
                        permissoesRoles.Add(new RolePermission
                        {
                            ModuleId = menu.Id,
                            SubModuleId = subMenu.Id,
                            PermissionId = permissao.Id,
                            RoleId = perfilAdm.Id
                        });
                    }
                }
            }

            return permissoesRoles;
        }
    }
}