using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Receita;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class ReceitaRepositories : BaseRepositories<Receita>, IReceitaRepositories
    {
        private DbSet<Receita> _dataset;
        private readonly IMapper _mapper;

        public ReceitaRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<Receita>();
            _mapper = mapper;
        }

        public async Task<PagedResult<Receita>> GetPageAsync(ReceitaFilterInput input)
        {
            return await _dataset
                        .Include(x => x.ReceitaLancamentos)
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Search) || x.Descricao.ToLower().Contains(input.Search.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal)
                               ).OrderByDescending(o => o.Id).ThenByDescending(t => t.DataCriacao)
                               .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }
    }
}
