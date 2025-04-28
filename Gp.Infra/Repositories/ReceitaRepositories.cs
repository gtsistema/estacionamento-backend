using AutoMapper;
using Gp.Domain.Extensions;
using Gp.Domain.Input.Receita;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Models;
using Gp.Domain.Shared;
using Gp.Infra.Context;
using Gp.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gp.Infra.Repositories
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

        public async Task AtualizarSaldoPagoAsync(Receita receita, ReceitaLancamento receitaLancamento)
        {
            receita.ValorTotal += receitaLancamento.ValorTotal;
            receita.SaldoRestante = receita.ValorPrevisto - receita.ValorTotal;
            receita.PorcentagemPaga = receita.ValorTotal / receita.ValorPrevisto * 100;

            await UpdateAsync(receita);
        }
    }
}
