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
                    .Include(x => x.SubModules)
                        .ThenInclude(x => x.Permissions)
                    .OrderBy(o => o.Ordem)
                    .ToListAsync();

            foreach (var menu in result)
            {
                menu.SubModules = (menu.SubModules ?? Enumerable.Empty<SubModule>())
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

        public async Task<List<MenuAcessOuput>> BuscarMenuUsuarioLogin(int roleId)
        {
            var dados = await _context.Set<RolePermission>()
                .Include(x => x.Module)
                .Include(x => x.SubModule)
                .Include(x => x.Permission)
                .Where(x => x.RoleId == roleId && x.Module != null && x.Module.Ativo)
                .ToListAsync();

            var dadosTransformados = dados.Select(x => new
             {
                 Menu = x.Module == null ? null : new
                 {
                     x.Module.Id,
                     x.Module.Descricao,
                     x.Module.Ativo,
                     x.Module.Rota,
                     x.Module.Ordem
                 },
                 SubMenu = x.SubModule == null || !x.SubModule.Ativo
                     ? null
                     : new
                 {
                     x.SubModule.Id,
                     x.SubModule.Descricao,
                     x.SubModule.Rota,
                     x.SubModule.Ativo,
                     x.SubModule.Ordem,
                     x.SubModule.ModuleId
                 }
             })
                .OrderBy(x => x.Menu?.Ordem)
                .ThenBy(x => x.SubMenu?.Ordem)
                .ToList();

            var menusDict = new Dictionary<int, MenuAcessOuput>();

            foreach (var item in dadosTransformados)
            {
                if (item.Menu == null)
                    continue;

                if (!menusDict.TryGetValue(item.Menu.Id, out var menu))
                {
                    menu = new MenuAcessOuput
                    {
                        Id = item.Menu.Id,
                        Descricao = item.Menu.Descricao,
                        Ativo = item.Menu.Ativo,
                        Rota = item.Menu.Rota,
                        Ordem = item.Menu.Ordem,
                        SubMenus = new List<SubMenuAcessOuput>()
                    };

                    menusDict.Add(menu.Id, menu);
                }

                if (item.SubMenu != null)
                {
                    // evita duplicidade
                    if (!menu.SubMenus.Any(s => s.Id == item.SubMenu.Id))
                    {
                        menu.SubMenus.Add(new SubMenuAcessOuput
                        {
                            Id = item.SubMenu.Id,
                            MenuId = item.SubMenu.ModuleId,
                            Descricao = item.SubMenu.Descricao,
                            Rota = item.SubMenu.Rota,
                            Ativo = item.SubMenu.Ativo,
                            Ordem = item.SubMenu.Ordem
                        });
                    }
                }
            }

            return menusDict.Values.ToList();
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

        public async Task DeletarSubMenu(SubModule subModule)
        {
             _context.SubModule.Remove(subModule);
             
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPermissao(Permission subModule)
        {
            _context.Permission.Remove(subModule);

            await _context.SaveChangesAsync();
        }

        public async Task<SubModule> SelecionarSubModulePorId(int id)
        {
            return await _context.SubModule.Include(x => x.Permissions)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<Permission> SelecionarPermissaoPorId(int id)
        {
            return await _context.Permission
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }

    }
}
