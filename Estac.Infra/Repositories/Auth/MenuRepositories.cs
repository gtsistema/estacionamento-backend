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
    public class MenuRepositories : BaseRepositoriesNone<Module>, IMenuRepositories
    {
        private DbSet<Module> _dataset;
        private readonly IMapper _mapper;
        private readonly IDapperRepositories _dapperRepositories;
        private readonly IdentityContext _identityContext;
        public MenuRepositories(GtsContext context, IMapper _mapper, IDapperRepositories _dapperRepositories,
            IdentityContext _identityContext) : base(context)
        {
            this._mapper = _mapper;
            _dataset = _identityContext.Set<Module>();
            this._dapperRepositories = _dapperRepositories;
            this._identityContext = _identityContext;
        }

        public async Task<Module> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.SubModules).ThenInclude(x => x.Permissions)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<MenuSearchOutput>> Paginar(MenuFilterInput input)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.SubModules)
                        .Include(x => x.SubModules).ThenInclude(x => x.Permissions)
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower()))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.SubModules.OrderBy(x => x.Ordem))
                         .Select(x => new MenuSearchOutput
                         {
                             Id = x.Id,
                             Descricao = x.Descricao,
                            
                         })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

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

        //public async Task<MenuOuput> BuscarMenuUsuario(int roleId)
        //{
        //    var resultado =  await _context.Set<RolePermission>
        //              .AsNoTracking()
        //              .Include(x => x.SubModules)
        //              .ThenInclude(x => x.Permissions)
        //              .SingleOrDefaultAsync(x => x. == usuarioId);


        //    _dataset.


        //    return resultado;
        //}
    }
}
