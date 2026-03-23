using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Auth;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class MenuRepositories : BaseRepositoriesNone<Module>, IMenuRepositories
    {
        private DbSet<Module> _dataset;
        private readonly IMapper _mapper;

        public MenuRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Module>();
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
    }
}
