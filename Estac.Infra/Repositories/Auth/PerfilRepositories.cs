using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Auth;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Domain.Models;
using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Text;

namespace Estac.Infra.Repositories.Auth
{
    public class PerfilRepositories : IPerfilRepositories
    {
        private DbSet<Veiculo> dataset;
        private readonly IMapper mapper;
        private readonly IDapperRepositories dapperRepositories;

        public PerfilRepositories(GtsContext context, IMapper mapper,
            IDapperRepositories dapperRepositories)
        {
            this.mapper = mapper;
            this.dataset = context.Set<Veiculo>();
            this.dapperRepositories = dapperRepositories;
        }

        public async Task<UsuarioRoleOuput> BuscarPerfilPorUsuarioToken(int usuarioId)
        {
            var userRole = await dapperRepositories.QueryFirstOrDefaultAsync<UsuarioRoleOuput>(
                    $@" SELECT
                	    u.Id as UserId, UserName, FullName as Nome, EstacionamentoId, Email, r.Id as RoleId, r.Name as Role 
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

            var menus = (await dapperRepositories.QueryAsync<MenuOuput>(
                "SELECT Id, Descricao, Rota FROM dbo.Module")).ToList();

            var subMenus = (await dapperRepositories.QueryAsync<SubMenuOuput>(
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

        private void MontarArvorePerfilPermissoesMenus(IEnumerable<MenuOuput> menus, IEnumerable<SubMenuOuput> subMenus, IEnumerable<PermissionOutput> permissions)
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
    }
}
