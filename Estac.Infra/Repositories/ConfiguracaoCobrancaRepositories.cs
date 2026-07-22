using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.ConfiguracaoCobranca;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.ConfiguracaoCobranca;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class ConfiguracaoCobrancaRepositories : BaseRepositories<ConfiguracaoCobranca>, IConfiguracaoCobrancaRepositories
    {
        private readonly DbSet<ConfiguracaoCobranca> _dataset;

        public ConfiguracaoCobrancaRepositories(GtsContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<ConfiguracaoCobranca>();
        }

        public async Task<ConfiguracaoCobranca> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                .AsNoTracking()
                .Include(x => x.Regra)
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<ConfiguracaoCobrancaSearchOutput>> Paginar(ConfiguracaoCobrancaFilterInput input)
        {
            var query = _dataset
                .AsNoTracking()
                .Include(x => x.Transportadora)
                .Include(x => x.Estacionamento)
                .AsQueryable();

            if (input.TransportadoraId.HasValue)
                query = query.Where(x => x.TransportadoraId == input.TransportadoraId.Value);

            if (input.EstacionamentoId.HasValue)
                query = query.Where(x => x.EstacionamentoId == input.EstacionamentoId.Value);

            if (input.Status.HasValue)
                query = query.Where(x => x.Status == input.Status.Value);

            if (!string.IsNullOrWhiteSpace(input.Descricao))
            {
                var termo = input.Descricao.Trim().ToLower();
                query = query.Where(x =>
                    (x.EmailFinanceiro != null && x.EmailFinanceiro.ToLower().Contains(termo))
                    || (x.Transportadora != null && x.Transportadora.Descricao != null && x.Transportadora.Descricao.ToLower().Contains(termo))
                    || (x.Estacionamento != null && x.Estacionamento.Descricao != null && x.Estacionamento.Descricao.ToLower().Contains(termo)));
            }

            if (input.DataInicial.HasValue)
                query = query.Where(x => x.DataCriacao.Date >= input.DataInicial.Value.Date);

            if (input.DataFinal.HasValue)
                query = query.Where(x => x.DataCriacao.Date <= input.DataFinal.Value.Date);

            return await query
                .OrderByDescending(x => x.DataCriacao)
                .Select(x => new ConfiguracaoCobrancaSearchOutput
                {
                    Id = x.Id,
                    TransportadoraId = x.TransportadoraId,
                    TransportadoraNome = x.Transportadora.Descricao,
                    EstacionamentoId = x.EstacionamentoId,
                    EstacionamentoNome = x.Estacionamento.Descricao,
                    Status = x.Status,
                    ModalidadeCobranca = x.ModalidadeCobranca,
                    EmailFinanceiro = x.EmailFinanceiro,
                    DataCriacao = x.DataCriacao
                })
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }

        public Task<bool> ExistePorTransportadoraEstacionamentoAsync(int transportadoraId, int estacionamentoId, int? ignorarId = null)
        {
            return _dataset
                .AsNoTracking()
                .AnyAsync(x =>
                    x.TransportadoraId == transportadoraId
                    && x.EstacionamentoId == estacionamentoId
                    && (!ignorarId.HasValue || x.Id != ignorarId.Value));
        }

        public override async Task<ConfiguracaoCobranca> Alterar(ConfiguracaoCobranca item)
        {
            try
            {
                var incomingRegra = item.Regra;
                item.Regra = null;

                var result = await _dataset
                    .Include(x => x.Regra)
                    .SingleOrDefaultAsync(p => p.Id == item.Id);

                if (result == null)
                    return null;

                item.DataCriacao = result.DataCriacao;
                item.DataAtualizacao = DateTime.Now;
                _context.Entry(result).CurrentValues.SetValues(item);

                if (incomingRegra != null)
                {
                    if (result.Regra != null)
                    {
                        var dataCriacao = result.Regra.DataCriacao;
                        incomingRegra.Id = result.Regra.Id;
                        incomingRegra.ConfiguracaoCobrancaId = result.Id;
                        incomingRegra.DataCriacao = dataCriacao;
                        incomingRegra.DataAtualizacao = DateTime.Now;
                        _context.Entry(result.Regra).CurrentValues.SetValues(incomingRegra);
                    }
                    else
                    {
                        incomingRegra.Id = 0;
                        incomingRegra.ConfiguracaoCobrancaId = result.Id;
                        incomingRegra.DataCriacao = DateTime.Now;
                        await _context.Set<ConfiguracaoCobrancaRegra>().AddAsync(incomingRegra);
                    }
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task Remove(int id)
        {
            try
            {
                var configuracao = await _dataset
                    .Include(x => x.Regra)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (configuracao is null)
                    return;

                _context.Remove(configuracao);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
