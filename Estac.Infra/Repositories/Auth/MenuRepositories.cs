using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Repositories.Dapper;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Estac.Infra.Repositories
{
    public class MenuRepositories : BaseRepositoriesIdentityNone<Module>, IMenuRepositories
    {
        private readonly DbSet<Module> _dataset;
        private readonly IMapper _mapper;
        private readonly IDapperRepositories _dapperRepositories;
        private readonly IdentityContext _context;

        public MenuRepositories(
            IdentityContext _context,
            IMapper mapper,
            IDapperRepositories dapperRepositories
        ) : base(_context)
        {
            this._context = _context;
            this._mapper = mapper;
            this._dapperRepositories = dapperRepositories;
            this._dataset = _context.Set<Module>();
        }

        public async Task<Module> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(m => new Module
                    {
                        Id = m.Id,
                        Descricao = m.Descricao,
                        Ordem = m.Ordem,

                        SubModules = m.SubModules
                            .OrderBy(sm => sm.Ordem)
                            .Select(sm => new SubModule
                            {
                                Id = sm.Id,
                                Descricao = sm.Descricao,
                                Ordem = sm.Ordem,

                                Permissions = sm.Permissions
                                    .OrderBy(p => p.Ordem)
                                    .ToList()
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Module>> Buscar()
        {
            var result = await _dataset
                    .AsNoTracking()
                    .Include(x => x.SubModules)
                        .ThenInclude(x => x.Permissions)
                    .OrderBy(o => o.Ordem)
                    .ToListAsync();

                foreach (var menu in result)
                {
                    menu.SubModules = menu.SubModules
                        .OrderBy(x => x.Ordem)
                        .ToList();
                }

            return result;
        }

        public async Task AtualizarOrdem(List<MenuOrdemInput> menus, List<SubMenuOrdemInput> subMenus)
        {
            if (menus.Any())
            {
                var sqlMenu = new StringBuilder();

                sqlMenu.AppendLine("UPDATE Module SET Ordem = CASE Id");

                foreach (var item in menus)
                {
                    sqlMenu.AppendLine($"WHEN {item.Id} THEN {item.Ordem}");
                }

                sqlMenu.AppendLine("END");
                sqlMenu.AppendLine($"WHERE Id IN ({string.Join(",", menus.Select(x => x.Id))})");

                await _dapperRepositories.ExecuteAsync(sqlMenu.ToString());
            }

            if (subMenus.Any())
            {
                var sqlSubMenu = new StringBuilder();

                sqlSubMenu.AppendLine("UPDATE SubModule SET Ordem = CASE Id");

                foreach (var item in subMenus)
                {
                    sqlSubMenu.AppendLine($"WHEN {item.Id} THEN {item.Ordem}");
                }

                sqlSubMenu.AppendLine("END");
                sqlSubMenu.AppendLine($"WHERE Id IN ({string.Join(",", subMenus.Select(x => x.Id))})");

                await _dapperRepositories.ExecuteAsync(sqlSubMenu.ToString());
            }
        }

        public async Task<List<MenuAcessOuput>> BuscarMenuUsuario(int roleId)
        {
            var menus = await _context.Set<RolePermission>()
                .Where(x => x.RoleId == roleId)
                .Select(x => new
                {
                    Menu = x.SubModule.Module,
                    SubMenu = x.SubModule,
                    Permission = x
                })
                .ToListAsync();

            var resultado = menus
                .OrderBy(x => x.Menu.Ordem)
                .GroupBy(x => x.Menu)
                .Select(menuGroup => new MenuAcessOuput
                {
                    Id = menuGroup.Key.Id,
                    Descricao = menuGroup.Key.Descricao,
                    Ativo = menuGroup.Key.Ativo,

                    Ordem = menuGroup.Key.Ordem,
                    SubMenus = menuGroup
                        .OrderBy(x => x.SubMenu.Ordem)
                        .GroupBy(x => x.SubMenu)
                        .Select(subMenuGroup => new SubMenuAcessOuput
                        {
                            Id = subMenuGroup.Key.Id,
                            MenuId = subMenuGroup.Key.ModuleId,
                            Descricao = subMenuGroup.Key.Descricao,
                            Rota = subMenuGroup.Key.Rota,
                            Ativo = subMenuGroup.Key.Ativo,
                            Ordem = subMenuGroup.Key.Ordem,
                            //Permissions = subMenuGroup
                            //    .Select(p => new PermissionOutput
                            //    {
                            //        Id = p.Permission.Id,
                            //        SubMenuId = p.Permission.SubModuleId,
                            //        Descricao = p.Permission.
                            //    })
                            //    .OrderBy(p => p.Ordem)
                            //    .ToList()
                        })
                        .ToList()
                })
                .ToList();

            return resultado;
        }

        public async Task Atualizar(Module menu)
        {
            var entity = await _context.Module.FindAsync(menu.Id);
            _context.Entry(entity).CurrentValues.SetValues(menu);

            var result = await _context.SaveChangesAsync();
        }

        public async Task AtualizarSubMenu(SubModule subModule)
        {
            var entity = await _context.SubModule.FindAsync(subModule.Id);
            _context.Entry(entity).CurrentValues.SetValues(subModule);

            var result = await _context.SaveChangesAsync();
        }

        public async Task AtualizarPermissao(Permission permission)
        {
            var entity = await _context.Permission.FindAsync(permission.Id);
            _context.Entry(entity).CurrentValues.SetValues(permission);

            var result = await _context.SaveChangesAsync();
        }

        public async Task GravarSubMenu(SubModule subModule)
        {
            await _context.SubModule.AddAsync(subModule);

            var result = await _context.SaveChangesAsync();
        }

        public async Task GravarPermissao(Permission permission)
        {
            await _context.Permission.AddAsync(permission);

            var result = await _context.SaveChangesAsync();
        }

        public async Task<Module> SelecionarPorId(int id)
        {
            return await _dataset
                    .Include(x => x.SubModules).ThenInclude(x => x.Permissions)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task Deletar(Module module)
        {
            _context.Module.Remove(module);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarSubMenu(IEnumerable<SubModule> subModule)
        {
             _context.SubModule.RemoveRange(subModule);
             
            await _context.SaveChangesAsync();
        }


    }
}
