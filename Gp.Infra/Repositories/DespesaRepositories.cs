using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Extensions;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Models;
using Gp.Domain.Shared;
using Gp.Infra.Context;
using Gp.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gp.Infra.Repositories
{
    public class DespesaRepositories : BaseRepositories<Despesa>, IDespesaRepositories
    {
        private DbSet<Despesa> _dataset;
        private readonly IMapper _mapper;

        public DespesaRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<Despesa>();
            _mapper = mapper;
        }

        public async Task<PagedQuery<Despesa>> GetPageAsync(DespesaFilterInput input)
        {
            return await _dataset
                        .Include(x => x.DespesaLancamentos)
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Search) || x.Descricao.ToLower().Contains(input.Search.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal)
                               ).OrderByDescending(o => o.Id).ThenByDescending(t => t.DataCriacao)
                               .ToPagedQuery(input.NumeroPagina, input.TamanhoPagina);
        }
    }
}
