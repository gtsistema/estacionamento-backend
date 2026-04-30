using Estac.Domain.Extensions;
using Estac.Domain.Input.EntradaSaida;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.EntradaSaida;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class EntradaSaidaRepositories : BaseRepositories<EntradaSaida>, IEntradaSaidaRepositories
    {
        private readonly DbSet<EntradaSaida> _dataset;

        public EntradaSaidaRepositories(GtsContext context) : base(context)
        {
            _dataset = context.Set<EntradaSaida>();
        }

        public async Task<EntradaSaida> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                .AsNoTracking()
                .Include(x => x.Motorista).ThenInclude(x => x.Pessoa)
                .Include(x => x.Transportadora).ThenInclude(x => x.Pessoa)
                .Include(x => x.Veiculo).ThenInclude(x => x.VeiculoDetalhe)
                .Include(x => x.Veiculo).ThenInclude(x => x.VeiculoModelo).ThenInclude(x => x.VeiculoMarca)
                .Include(x => x.Suspensoes)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EntradaSaida> SelecionarParaControlePermanencia(int id)
        {
            return await _dataset
                .Include(x => x.Suspensoes)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EntradaSaida> SelecionarPorPlaca(string placa)
        {
            var placaNormalizada = placa.Trim().ToLower();

            return await _dataset
                .AsNoTracking()
                .Include(x => x.Motorista).ThenInclude(x => x.Pessoa)
                .Include(x => x.Transportadora).ThenInclude(x => x.Pessoa)
                .Include(x => x.Veiculo).ThenInclude(x => x.VeiculoDetalhe)
                .Include(x => x.Veiculo).ThenInclude(x => x.VeiculoModelo).ThenInclude(x => x.VeiculoMarca)
                .Include(x => x.Suspensoes)
                .Where(x => x.Veiculo.Placa.ToLower() == placaNormalizada)
                .OrderByDescending(x => x.DataHoraEntrada)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<EntradaSaidaSearchOutput>> Paginar(EntradaSaidaFilterInput input)
        {
            return await _dataset
                .AsNoTracking()
                .Include(x => x.Motorista).ThenInclude(x => x.Pessoa)
                .Include(x => x.Transportadora).ThenInclude(x => x.Pessoa)
                .Include(x => x.Veiculo)
                .Where(x =>
                    (string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower())) &&
                    (string.IsNullOrEmpty(input.Placa) || x.Veiculo.Placa.ToLower().Contains(input.Placa.ToLower())) &&
                    (!input.MotoristaId.HasValue || x.MotoristaId == input.MotoristaId.Value) &&
                    (!input.TransportadoraId.HasValue || x.TransportadoraId == input.TransportadoraId.Value) &&
                    (!input.SomenteEmAberto || !x.DataHoraSaida.HasValue) &&
                    (
                        (!input.DataInicial.HasValue && !input.DataFinal.HasValue) ||
                        (
                            x.DataHoraEntrada.Date >= input.DataInicial.Value.Date &&
                            x.DataHoraEntrada.Date <= input.DataFinal.Value.Date
                        )
                    ))
                .OrderByDescending(x => x.DataHoraEntrada)
                .Select(x => new EntradaSaidaSearchOutput
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    MotoristaId = x.MotoristaId,
                    NomeMotorista = x.Motorista.Pessoa.Descricao,
                    TransportadoraId = x.TransportadoraId,
                    NomeTransportadora = x.Transportadora.Pessoa.Descricao,
                    VeiculoId = x.VeiculoId,
                    PlacaVeiculo = x.Veiculo.Placa,
                    DataHoraEntrada = x.DataHoraEntrada,
                    DataHoraSaida = x.DataHoraSaida
                })
                .GetPaged(input.NumeroPagina, input.TamanhoPagina);
        }
    }
}
