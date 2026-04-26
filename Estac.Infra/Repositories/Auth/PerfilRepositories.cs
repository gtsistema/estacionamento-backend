using AutoMapper;
using Estac.Domain.Dto.Perfil;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Estac.Infra.Repositories.Auth
{
    public class PerfilRepositories : IPerfilRepositories
    {
        private readonly IMapper mapper;
        private readonly IDapperRepositories dapperRepositories;
        private readonly IdentityContext _context;

        public PerfilRepositories(IdentityContext _context, IMapper mapper,
            IDapperRepositories dapperRepositories)
        {
            this.mapper = mapper;
            this._context = _context;
            this.dapperRepositories = dapperRepositories;
        }


        public async Task<ApplicationRole> BuscarPerfilPorId(int roleId)
        {
            return await _context.Set<ApplicationRole>().FirstOrDefaultAsync(rp => rp.Id == roleId);
        }

        public async Task<UsuarioRoleOuput> BuscarPerfilPorUsuarioToken(int usuarioId)
        {
            var userRole = await dapperRepositories.QueryFirstOrDefaultAsync<UsuarioRoleOuput>(
                    $@" SELECT
                	    u.Id as UserId, UserName, FullName as Nome, u.EstacionamentoId, u.TransportadoraId, Email, r.Id as RoleId, r.Name as Role 
                    FROM dbo.UserRole ur
                    INNER JOIN dbo.[USER] u ON ur.UserId = u.Id
                    INNER JOIN dbo.[ROLE] r ON r.id = ur.RoleId
                    WHERE ur.UserId = @Id",
                        new { Id = usuarioId });

            userRole.Permissions = (await dapperRepositories.QueryAsync<PermissionOutput>(
                    @"SELECT p.Id, p.Ordem, p.Acao as Descricao, rp.SubModuleId as SubMenuId
                          FROM dbo.Permission p
                          INNER JOIN dbo.RolePermission rp ON rp.PermissionId = p.Id
                          WHERE rp.RoleId = @RoleId
                        ORDER BY P.Ordem",
                  new { RoleId = userRole.RoleId })).ToList();

            return userRole;
        }

        public async Task<UsuarioAcessoPerfilOutput> BuscarPerfilPermissaoUsuario(int usuarioId)
        {
            var usuario = await dapperRepositories.QueryFirstOrDefaultAsync<UsuarioOutput>(
                "SELECT Id, userName, FullName as Nome, EstacionamentoId, Email FROM dbo.[USER] WHERE Id = @Id",
                new { Id = usuarioId });

            var role = await dapperRepositories.QueryFirstOrDefaultAsync<RoleOutput>(
                @"SELECT r.Id, r.Name as Descricao 
                  FROM dbo.[ROLE] r
                  INNER JOIN dbo.UserRole ur ON ur.RoleId = r.Id
                  WHERE ur.UserId = @Id",
                        new { Id = usuarioId });

            var menus = (await dapperRepositories.QueryAsync<MenuOutput>(
                "SELECT Id, Descricao, Rota FROM dbo.Module")).ToList();

            var subMenus = (await dapperRepositories.QueryAsync<SubMenuOutput>(
                "SELECT Id, Descricao, Rota, ModuleId as MenuId FROM dbo.SubModule")).ToList();

            var permissions = (await dapperRepositories.QueryAsync<PermissionOutput>(
                    @"SELECT p.Id, p.Ordem, p.Acao as Descricao,  rp.SubModuleId as SubMenuId
                          FROM dbo.Permission p
                          INNER JOIN dbo.RolePermission rp ON rp.PermissionId = p.Id
                          WHERE rp.RoleId = @RoleId
                        ORDER BY P.Ordem",
                  new { RoleId = role.Id })).ToList();

            MontarArvorePerfilPermissoesMenus(menus, subMenus, permissions);

            return new UsuarioAcessoPerfilOutput
            {
                Usuario = usuario,
                Role = role,
                Menus = menus
            };
        }

        private void MontarArvorePerfilPermissoesMenus(IEnumerable<MenuOutput> menus, IEnumerable<SubMenuOutput> subMenus, IEnumerable<PermissionOutput> permissions)
        {
            foreach (var menu in menus)
            {
                var subs = subMenus.Where(s => s.MenuId == menu.Id).ToList();

                if (subs.Any())
                {
                    foreach (var sub in subs)
                    {
                        sub.Permissions = permissions
                            .Where(p => p.SubMenuId == sub.Id)
                            .ToList();
                    }
                }

                menu.SubMenus = subs;
            }
        }

        public async Task<PerfilOutput> BuscarTodosPerfilMenu(int perfilId)
        {

            var role = await dapperRepositories.QueryFirstOrDefaultAsync<PerfilOutput>(
              @"SELECT r.Id, r.Name as Descricao 
                  FROM dbo.[ROLE] r
                  WHERE ur.RoleId = @Id",
                      new { Id = perfilId });

            var menus = (await dapperRepositories.QueryAsync<MenuOutput>(
              "SELECT Id, Descricao, Rota FROM dbo.Module")).ToList();

            var subMenus = (await dapperRepositories.QueryAsync<SubMenuOutput>(
                "SELECT Id, Descricao, Rota, ModuleId as MenuId FROM dbo.SubModule")).ToList();

            var permissions = (await dapperRepositories.QueryAsync<PermissionOutput>(
                    @"SELECT p.Id, p.Ordem, p.Acao as Descricao,  rp.SubModuleId as SubMenuId
                          FROM dbo.Permission p
                          INNER JOIN dbo.RolePermission rp ON rp.PermissionId = p.Id
                          WHERE rp.RoleId = @RoleId
                        ORDER BY P.Ordem",
                  new { RoleId = perfilId })).ToList();

            MontarArvorePerfilPermissoesMenus(menus, subMenus, permissions);

            return new PerfilOutput
            {
                Descricao = role.Descricao,
                Id = role.Id,
                Menus = menus
            };
        }

        public async Task AtualizarPermissoesDoPerfil(IEnumerable<RolePermission> rolePermissionsGravar, IEnumerable<RolePermission> rolePermissionsAtuais)
        {
            if (rolePermissionsAtuais != null && rolePermissionsAtuais.Any())
                    RemoveTodasPermissoesDoPerfil(rolePermissionsAtuais);

            if (rolePermissionsGravar != null && rolePermissionsGravar.Any())
                    await AdicionarPermissoesPerfil(rolePermissionsGravar);
        }

        public void RemoveTodasPermissoesDoPerfil(IEnumerable<RolePermission> rolePermissions)
        {
            _context.Set<RolePermission>().RemoveRange(rolePermissions);
        }

        public async Task AdicionarPermissoesPerfil(IEnumerable<RolePermission> rolePermissions)
        {
             await _context.Set<RolePermission>().AddRangeAsync(rolePermissions);
        }

        public async Task<IEnumerable<RolePermission>> BuscarRolePermissoes(int perfilId)
        {
            return await _context.Set<RolePermission>().Where(rp => rp.RoleId == perfilId).ToListAsync();
        }

        public async Task AdicionarPerfilSimples(ApplicationRole role)
        {
            await _context.Set<ApplicationRole>().AddRangeAsync(role);
            await SaveChangesAsync();
        }

        public async Task AtualizarPerfilSimplesAsync(ApplicationRole role)
        {
            role.NormalizedName = role.Name.ToUpper();

            var entry = await _context.Set<ApplicationRole>().Where(r => r.Id == role.Id).ExecuteUpdateAsync(setters =>
                setters.SetProperty(r => r.Name, role.Name).SetProperty(r => r.NormalizedName, role.NormalizedName));
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<PerfilDto>> BuscarPerfilPermissaoGrid(int? roleId)
        {
            var sql = @"WITH Perfis AS (
                                        SELECT 
                                            r.Id AS PerfilId,
                                            r.Name AS Perfil
                                        FROM Role r
                                        WHERE @roleId IS NULL OR r.Id = @roleId
                                    ),

                                    PermissoesDoPerfil AS (
                                        SELECT 
                                            rp.RoleId,
                                            rp.ModuleId,
                                            rp.SubModuleId,
                                            rp.PermissionId
                                        FROM RolePermission rp
                                        WHERE @roleId IS NULL OR rp.RoleId = @roleId
                                    ),

                                    EstruturaCompleta AS (
                                        SELECT 
                                            m.Id AS MenuId,
                                            m.Descricao,
                                            m.Ordem AS MenuOrdem,
                                            m.Ativo,
                                            m.Rota,

                                            sm.Id AS SubMenuId,
                                            sm.Descricao AS SubDescricao,
                                            sm.Ordem AS SubOrdem,
                                            sm.Ativo AS SubAtivo,
                                            sm.Rota AS SubRota,

                                            p.Id AS PermissaoId,
                                            p.Ordem AS PermissaoOrdem,
                                            p.Acao

                                        FROM Module m
                                        LEFT JOIN SubModule sm ON sm.ModuleId = m.Id
                                        LEFT JOIN Permission p ON p.SubModuleId = sm.Id
                                    )

                                    SELECT 
                                        pf.PerfilId,
                                        pf.Perfil,

                                        ec.MenuId,
                                        ec.Descricao,
                                        ec.MenuOrdem,
                                        ec.Ativo,
                                        ec.Rota,  

                                        -- Menu (só existe se tiver sub + permissão)
                                        CASE 
                                            WHEN EXISTS (
                                                SELECT 1 
                                                FROM RolePermission rp
                                                WHERE rp.RoleId = pf.PerfilId
                                                  AND rp.ModuleId = ec.MenuId
                                            ) 
                                            THEN 1 ELSE 0 
                                        END AS Selecionado,

                                         -- SubMenu

                                        ec.SubMenuId,
                                        ec.SubDescricao,
                                        ec.SubOrdem,
                                        ec.SubAtivo,
                                        ec.SubRota,

                                        -- SubMenu (só existe se tiver permissão vinculada)
                                        CASE 
                                            WHEN EXISTS (
                                                SELECT 1 
                                                FROM RolePermission rp
                                                WHERE rp.RoleId = pf.PerfilId
                                                  AND rp.ModuleId = ec.MenuId
                                                  AND rp.SubModuleId = ec.SubMenuId
                                            ) 
                                            THEN 1 ELSE 0 
                                        END AS SubSelecionado,

                                        ec.PermissaoId,
                                        ec.PermissaoOrdem,
                                        ec.Acao,

                                        -- Permissão (nível mais baixo)
                                        CASE 
                                            WHEN EXISTS (
                                                SELECT 1 
                                                FROM RolePermission rp
                                                WHERE rp.RoleId = pf.PerfilId
                                                  AND rp.ModuleId = ec.MenuId
                                                  AND rp.SubModuleId = ec.SubMenuId
                                                  AND rp.PermissionId = ec.PermissaoId
                                            ) 
                                            THEN 1 ELSE 0 
                                        END AS PermSelecionado

                                    FROM Perfis pf

                                    CROSS JOIN EstruturaCompleta ec

                                    LEFT JOIN PermissoesDoPerfil ppm 
                                        ON ppm.RoleId = pf.PerfilId 
                                        AND ppm.ModuleId = ec.MenuId

                                    LEFT JOIN PermissoesDoPerfil pps 
                                        ON pps.RoleId = pf.PerfilId 
                                        AND pps.SubModuleId = ec.SubMenuId

                                    LEFT JOIN PermissoesDoPerfil ppp 
                                        ON ppp.RoleId = pf.PerfilId 
                                        AND ppp.PermissionId = ec.PermissaoId

                                    ORDER BY 
                                        ec.MenuOrdem, 
                                        ec.SubOrdem, 
                                        ec.PermissaoOrdem;";

            var lookup = new Dictionary<string, PerfilDto>();

            var result = await dapperRepositories.QueryAsync<PerfilDto, MenuDto, SubMenuDto, PermissionDto, PerfilDto>(
                sql,
                (Func<PerfilDto, MenuDto, SubMenuDto, PermissionDto, PerfilDto>)((perfil, menu, subMenu, permission) =>
                {
                    if (!lookup.TryGetValue(perfil.PerfilId, out var perf))
                    {
                        perf = perfil;
                        perf.Menus = new List<MenuDto>();
                        lookup.Add(perf.PerfilId, perf);
                    }

                    // MENU
                    if (menu != null && menu.MenuId != 0)
                    {
                        var mod = perf.Menus.FirstOrDefault(m => m.MenuId == menu.MenuId);

                        if (mod == null)
                        {
                            mod = menu;
                            mod.SubMenus = new List<SubMenuDto>();
                            perf.Menus.Add(mod);
                        }

                        // SUBMENU
                        if (subMenu != null && subMenu.SubMenuId != 0)
                        {
                            var sub = mod.SubMenus.FirstOrDefault(s => s.SubMenuId == subMenu.SubMenuId);

                            if (sub == null)
                            {
                                sub = subMenu;
                                sub.Permissions = new List<PermissionDto>();
                                mod.SubMenus.Add(sub);
                            }

                            // PERMISSION
                            if (permission != null && permission.PermissaoId != 0)
                            {
                                if (!sub.Permissions.Any((Func<PermissionDto, bool>)(p => p.PermissaoId == permission.PermissaoId)))
                                {
                                    sub.Permissions.Add(permission);
                                }
                            }
                        }
                    }

                    return perf;
                }),
                new { roleId },
                splitOn: "PerfilId,MenuId,SubMenuId,PermissaoId"
            );

            return lookup.Values.ToList();
        }

        public async Task<Permission> BuscarPermissao(int subSubMenuId, string acao)
        {
            return await _context.Set<Permission>().FirstOrDefaultAsync(rp => rp.SubModuleId == subSubMenuId && rp.Acao.Equals(acao));
        }

        public async Task<IEnumerable<ApplicationRole>> BuscarSimplicado(int empresaId)
        {
            return await _context.Set<ApplicationRole>().Where(x => x.EmpresaId == empresaId || x.Padrao ).ToListAsync();
        }
    }
}