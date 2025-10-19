using AutoMapper;
using Estac.Domain.Dtos;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Despesa;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class DespesaRepositories : BaseRepositories<Despesa>, IDespesaRepositories
    {
        private DbSet<Despesa> _dataset;
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        public DespesaRepositories(GpContext context, IMapper mapper, IConfiguration configuration) : base(context)
        {
            _dataset = context.Set<Despesa>();
            _mapper = mapper;

            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<PagedResult<Despesa>> GetPageAsync(DespesaFilterInput input)
        {
            return await _dataset
                        .Include(x => x.DespesaLancamentos)
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Search) || x.Descricao.ToLower().Contains(input.Search.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal)
                               ).OrderByDescending(o => o.Id).ThenByDescending(t => t.DataCriacao)
                               .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }

        public async Task AtualizarSaldoPagoAsync(Despesa despesa, DespesaPagamento despesaPagamento)
        {
            despesa.ValorPago += despesaPagamento.ValorTotal;
            despesa.SaldoRestante = despesa.ValorTotal - despesa.ValorPago;
            despesa.PorcentagemPaga = despesa.ValorPago / despesa.ValorTotal * 100;

            if (despesa.ValorTotal > 0)
                despesa.PorcentagemPaga = (despesa.ValorPago / despesa.ValorTotal) * 100;
            else
                despesa.PorcentagemPaga = 0;

            await UpdateAsync(despesa);
        }

        public async Task AtualizarSaldoDevedorAsync(Despesa despesa, DespesaLancamento despesaLancamento)
        {
            despesa.ValorTotal += despesaLancamento.Valor;
            despesa.SaldoRestante = despesa.ValorTotal - despesa.ValorPago;

            if (despesa.ValorTotal > 0)
                despesa.PorcentagemPaga = (despesa.ValorPago / despesa.ValorTotal) * 100;
            else
                despesa.PorcentagemPaga = 0;

            await UpdateAsync(despesa);
        }

        public async Task AtualizarSaldoDevadorDoMesAsync(AtualizaSaldoDoMesPostInput input)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "EXEC AtualizarSaldoDespesaMes @MesReferente = {0}, @TipoDespesa = {1}",
               input.MesReferente,
               input.TipoDespesa);
        }
    }
}
