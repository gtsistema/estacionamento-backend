using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class VeiculoRepositories : BaseRepositories<Veiculo>, IVeiculoRepositories
    {
        private DbSet<Veiculo> _dataset;
        private readonly IMapper _mapper;

        public VeiculoRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Veiculo>();
        }

        public async Task<PagedResult<VeiculoSearchOutput>> Paginar(VeiculoFilterInput input)
        {
            var termoBusca = string.IsNullOrWhiteSpace(input.Descricao) ? null : input.Descricao.Trim().ToLower();
            var termoBuscaPlacaNorm = termoBusca == null ? null : VeiculoPlacaHelper.Normalizar(termoBusca);
            var placaFiltroNorm = string.IsNullOrWhiteSpace(input.Placa) ? null : VeiculoPlacaHelper.Normalizar(input.Placa);

            var result = await _dataset
                        .AsNoTracking()
                        .Where(x =>
                            (!input.TransportadoraId.HasValue
                                || (x.TransportadoraId != null && x.TransportadoraId == input.TransportadoraId.Value))
                            && (termoBusca == null
                                || (x.Descricao != null && x.Descricao.ToLower().Contains(termoBusca))
                                || (termoBuscaPlacaNorm != null && x.Placa != null && x.Placa.Contains(termoBuscaPlacaNorm))
                                || (x.VeiculoModelo != null && x.VeiculoModelo.Descricao != null
                                    && x.VeiculoModelo.Descricao.ToLower().Contains(termoBusca)))
                            && (placaFiltroNorm == null || (x.Placa != null && x.Placa.Contains(placaFiltroNorm))))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.DataCriacao)
                        .Select(x => new VeiculoSearchOutput 
                        {
                            Ano = x.Ano,
                            Ativo = x.Ativo,
                            Cor = x.Cor,
                            Id = x.Id,
                            Placa = x.Placa,
                            ModeloMarca = x.VeiculoModelo != null ? x.VeiculoModelo.Descricao + " - " + x.VeiculoModelo.VeiculoMarca.Descricao : null,
                            MotoristaId = x.MotoristaId,
                            Motorista = x.Motorista != null ? x.Motorista.Descricao : null,
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            foreach (var item in result.Results)
                item.Placa = VeiculoPlacaHelper.FormatarExibicao(item.Placa);

            return result;
        }

        public async Task<Veiculo> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.VeiculoDetalhe)
                        .Include(x => x.VeiculoModelo)
                            .ThenInclude(m => m.VeiculoMarca)
                        .Include(x => x.Motorista)
                            .ThenInclude(m => m.Pessoa)
                                .ThenInclude(p => p.Contatos)
                        .Include(x => x.Motorista)
                            .ThenInclude(m => m.Pessoa)
                                .ThenInclude(p => p.Enderecos)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Veiculo> GravarCompleto(Veiculo veiculo)
        {
            veiculo.Transportadora = null;
            veiculo.Motorista = null;
            veiculo.VeiculoModelo = null;

            veiculo.Placa = VeiculoPlacaHelper.Normalizar(veiculo.Placa);

            if (string.IsNullOrWhiteSpace(veiculo.Descricao))
                veiculo.Descricao = veiculo.Placa ?? string.Empty;

            veiculo.Id = 0;
            veiculo.DataCriacao = DateTime.Now;

            if (veiculo.VeiculoDetalhe != null)
            {
                veiculo.VeiculoDetalhe.Id = 0;
                veiculo.VeiculoDetalhe.VeiculoId = 0;
                if (string.IsNullOrWhiteSpace(veiculo.VeiculoDetalhe.Descricao))
                    veiculo.VeiculoDetalhe.Descricao = veiculo.Descricao;
            }

            await _context.AddAsync(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        }

        public async Task<Veiculo> AlterarCompleto(Veiculo dados)
        {
            var entity = await _dataset
                .Include(v => v.VeiculoDetalhe)
                .FirstOrDefaultAsync(v => v.Id == dados.Id);

            if (entity == null)
                return null;

            entity.Placa = VeiculoPlacaHelper.Normalizar(dados.Placa);
            entity.Ano = dados.Ano;
            entity.Ativo = dados.Ativo;
            entity.Cor = dados.Cor;
            entity.Descricao = string.IsNullOrWhiteSpace(dados.Descricao)
                ? entity.Placa ?? entity.Descricao
                : dados.Descricao;
            entity.TransportadoraId = dados.TransportadoraId;
            entity.MotoristaId = dados.MotoristaId;
            entity.VeiculoModeloId = dados.VeiculoModeloId;

            if (dados.VeiculoDetalhe != null)
            {
                var fallbackDesc = entity.Descricao ?? entity.Placa ?? string.Empty;

                if (entity.VeiculoDetalhe != null)
                {
                    CopiarCamposDetalhe(dados.VeiculoDetalhe, entity.VeiculoDetalhe, fallbackDesc);
                }
                else
                {
                    var novo = new VeiculoDetalhe { VeiculoId = entity.Id };
                    CopiarCamposDetalhe(dados.VeiculoDetalhe, novo, fallbackDesc);
                    entity.VeiculoDetalhe = novo;
                }
            }

            entity.DataAtualizacao = DateTime.Now;

            return entity;
        }

        private static void CopiarCamposDetalhe(VeiculoDetalhe origem, VeiculoDetalhe destino, string descricaoFallback)
        {
            destino.Uf = origem.Uf;
            destino.NomeProprietario = origem.NomeProprietario;
            destino.CpfCnpjProprietario = origem.CpfCnpjProprietario;
            destino.KmAtual = origem.KmAtual;
            destino.KmRastreador = origem.KmRastreador;
            destino.CapacidadeCombustivel = origem.CapacidadeCombustivel;
            destino.CapacidadeArla = origem.CapacidadeArla;
            destino.MediaMinima = origem.MediaMinima;
            destino.MediaMaxima = origem.MediaMaxima;
            destino.InscricaoEstadual = origem.InscricaoEstadual;
            destino.VeiculoTerceiro = origem.VeiculoTerceiro;
            destino.Observacoes = origem.Observacoes;
            destino.Descricao = string.IsNullOrWhiteSpace(origem.Descricao)
                ? descricaoFallback
                : origem.Descricao;
        }

        /// <summary>
        /// Exclui <see cref="VeiculoDetalhe"/> pelo <c>VeiculoId</c> e em seguida o <see cref="Veiculo"/>.
        /// </summary>
        public async Task<bool> ExcluirCompleto(int id)
        {
            var veiculo = await _dataset.FirstOrDefaultAsync(v => v.Id == id);
            if (veiculo == null)
                return false;

            var detalhes = await _context.VeiculoDetalhe
                .Where(d => d.VeiculoId == id)
                .ToListAsync();

            foreach (var d in detalhes)
                _context.Remove(d);

            _context.Remove(veiculo);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MotoristaVinculosPorPlacaOutput> ObterVinculosPorPlaca(string placa)
        {
            var placaNorm = VeiculoPlacaHelper.Normalizar(placa);
            if (string.IsNullOrEmpty(placaNorm))
                return null;

            var veiculo = await _context.Veiculo
                .AsNoTracking()
                .Include(v => v.Motorista)!.ThenInclude(m => m.Pessoa)
                .Include(v => v.Transportadora)!.ThenInclude(t => t.Pessoa)
                .FirstOrDefaultAsync(v => v.Placa != null && v.Placa == placaNorm);

            if (veiculo == null)
                return null;

            IReadOnlyList<TransportadoraVeiculoVinculoOutput> vinculos;
            if (veiculo.TransportadoraId.HasValue)
            {
                var tid = veiculo.TransportadoraId.Value;
                vinculos = await _context.Veiculo.AsNoTracking()
                    .Where(v => v.TransportadoraId == tid)
                    .OrderBy(v => v.Placa)
                    .Select(v => new TransportadoraVeiculoVinculoOutput
                    {
                        TransportadoraId = tid,
                        VeiculoId = v.Id,
                        Placa = v.Placa
                    })
                    .ToListAsync();

                foreach (var v in vinculos)
                    v.Placa = VeiculoPlacaHelper.FormatarExibicao(v.Placa);
            }
            else
            {
                vinculos = Array.Empty<TransportadoraVeiculoVinculoOutput>();
            }

            return new MotoristaVinculosPorPlacaOutput
            {
                Motorista = veiculo.Motorista != null ? _mapper.Map<MotoristaOutput>(veiculo.Motorista) : null,
                Veiculo = _mapper.Map<VeiculoVinculoResumoOutput>(veiculo),
                Transportadora = veiculo.Transportadora != null ? _mapper.Map<TransportadoraOutput>(veiculo.Transportadora) : null,
                VinculosTransportadoraVeiculo = vinculos
            };
        }
    }
}
