using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Veiculo;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;

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
                            Placa = VeiculoPlacaHelper.FormatarExibicao(x.Placa),
                            TipoCarga = x.TipoCarga,
                            Modelo = x.VeiculoModelo != null ? x.VeiculoModelo.Descricao : null,
                            Marca = x.VeiculoModelo != null && x.VeiculoModelo.VeiculoMarca != null ? x.VeiculoModelo.VeiculoMarca.Descricao : null,
                            Motoristas = x.VeiculoMotoristas
                                .Where(vm => vm.Motorista != null)
                                .OrderByDescending(vm => vm.Principal == true)
                                .ThenBy(vm => vm.Motorista.Descricao)
                                .Select(vm => new VeiculoMotoristaSearchOutput
                                {
                                    Id = vm.MotoristaId,
                                    Motorista = vm.Motorista.Descricao,
                                    Cpf = vm.Motorista.Pessoa != null ? vm.Motorista.Pessoa.Documento : null,
                                    Principal = vm.Principal
                                })
                                .ToList(),
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }

        public async Task<Veiculo> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.VeiculoDetalhe)
                        .Include(x => x.VeiculoModelo)
                            .ThenInclude(m => m.VeiculoMarca)
                        .Include(x => x.VeiculoMotoristas)
                            .ThenInclude(vm => vm.Motorista)
                                .ThenInclude(m => m.Pessoa)
                                    .ThenInclude(p => p.Contatos)
                        .Include(x => x.VeiculoMotoristas)
                            .ThenInclude(vm => vm.Motorista)
                                .ThenInclude(m => m.Pessoa)
                                    .ThenInclude(p => p.Enderecos)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Veiculo> GravarCompleto(Veiculo veiculo)
        {
            veiculo.Transportadora = null;
            veiculo.VeiculoModelo = null;
            veiculo.VeiculoMotoristas ??= new List<VeiculoMotorista>();

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

            foreach (var vinculo in veiculo.VeiculoMotoristas)
            {
                vinculo.Id = 0;
                vinculo.VeiculoId = 0;
                vinculo.Motorista = null;
            }

            await _context.AddAsync(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        }

        public async Task<Veiculo> AlterarCompleto(Veiculo dados)
        {
            var entity = await _dataset
                .Include(v => v.VeiculoDetalhe)
                .Include(v => v.VeiculoMotoristas)
                .FirstOrDefaultAsync(v => v.Id == dados.Id);

            if (entity == null)
                return null;

            entity.Placa = VeiculoPlacaHelper.Normalizar(dados.Placa);
            entity.Ano = dados.Ano;
            entity.Ativo = dados.Ativo;
            entity.Cor = dados.Cor;
            entity.TipoCarga = dados.TipoCarga;
            entity.Descricao = string.IsNullOrWhiteSpace(dados.Descricao)
                ? entity.Placa ?? entity.Descricao
                : dados.Descricao;
            entity.TransportadoraId = dados.TransportadoraId;
            entity.VeiculoModeloId = dados.VeiculoModeloId;
            AtualizarVinculosMotorista(entity, dados);

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

        private static void AtualizarVinculosMotorista(Veiculo entity, Veiculo dados)
        {
            var desejados = (dados.VeiculoMotoristas ?? new List<VeiculoMotorista>())
                .GroupBy(x => x.MotoristaId)
                .Select(g => g.Last())
                .ToDictionary(x => x.MotoristaId, x => x.Principal);

            var remover = entity.VeiculoMotoristas
                .Where(x => !desejados.ContainsKey(x.MotoristaId))
                .ToList();

            foreach (var item in remover)
                entity.VeiculoMotoristas.Remove(item);

            foreach (var existente in entity.VeiculoMotoristas)
            {
                if (desejados.TryGetValue(existente.MotoristaId, out var principal))
                    existente.Principal = principal;
            }

            var existentesIds = entity.VeiculoMotoristas
                .Select(x => x.MotoristaId)
                .ToHashSet();

            foreach (var (motoristaId, principal) in desejados)
            {
                if (existentesIds.Contains(motoristaId))
                    continue;

                entity.VeiculoMotoristas.Add(new VeiculoMotorista
                {
                    MotoristaId = motoristaId,
                    VeiculoId = entity.Id,
                    Principal = principal
                });
            }
        }

        public Task<bool> PossuiMotoristaVinculadoAsync(int veiculoId) =>
            _context.Set<VeiculoMotorista>().AsNoTracking().AnyAsync(vm => vm.VeiculoId == veiculoId);

        public Task<bool> PossuiVeiculoMotoristaNaTransportadoraAsync(int transportadoraId) =>
            _context.Set<VeiculoMotorista>().AsNoTracking()
                .AnyAsync(vm => vm.Veiculo != null && vm.Veiculo.TransportadoraId == transportadoraId);

        public Task<bool> PossuiVeiculoMotoristaParaMotoristaAsync(int motoristaId) =>
            _context.Set<VeiculoMotorista>().AsNoTracking().AnyAsync(vm => vm.MotoristaId == motoristaId);

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

        public async Task<EntradaSaidaVinculoOutput> ObterVinculosPorPlaca(string placa)
        {
            var placaNorm = VeiculoPlacaHelper.Normalizar(placa);
            if (string.IsNullOrEmpty(placaNorm))
                return null;

            var veiculo = await _context.Veiculo
                .AsNoTracking()
                .Include(v => v.VeiculoMotoristas)
                    .ThenInclude(vm => vm.Motorista)
                        .ThenInclude(m => m.Pessoa)
                .Include(v => v.Transportadora)!.ThenInclude(t => t.Pessoa)
                .FirstOrDefaultAsync(v => v.Placa != null && v.Placa == placaNorm);

            if (veiculo == null)
                return null;

            return new EntradaSaidaVinculoOutput
            {
                VeiculoId = veiculo.Id,
                Placa = VeiculoPlacaHelper.FormatarExibicao(veiculo.Placa),
                TipoCarga = veiculo.TipoCarga,
                TransportadoraId = veiculo.TransportadoraId,
                RazaoSocial = veiculo.Transportadora?.Pessoa?.NomeRazaoSocial,
                Cnpj = veiculo.Transportadora?.Pessoa?.Documento.FormatarCnpj(),
                ResponsavelLegal = veiculo.Transportadora?.ResponsavelLegal,
                ResponsavelCpf = veiculo.Transportadora?.ResponsavelCpf?.FormatarCpf(),
                ResponsavelEmail = veiculo.Transportadora?.ResponsavelEmail,
                ResponsavelTelefone = veiculo.Transportadora?.ResponsavelTelefone.FormatarTelefone(),
                Motorista = veiculo.VeiculoMotoristas
                    .Where(vm => vm.Motorista != null)
                    .OrderByDescending(vm => vm.Principal == true)
                    .ThenByDescending(vm => vm.Id)
                    .Select(vm => new EntradaSaidaMotoristaVinculoOutput
                    {
                        Id = vm.Motorista.Id,
                        Nome = vm.Motorista.Pessoa != null ? vm.Motorista.Pessoa.NomeRazaoSocial : vm.Motorista.Descricao,
                        Cpf = vm.Motorista.Pessoa != null ? vm.Motorista.Pessoa.Documento.FormatarCpf() : null,
                        Principal = vm.Principal
                    })
                    .FirstOrDefault()
            };
        }

        public async Task<bool> ExistePorPlacaAsync(string placa, int? ignorarVeiculoId = null)
        {
            var placaNorm = VeiculoPlacaHelper.Normalizar(placa);
            if (string.IsNullOrWhiteSpace(placaNorm))
                return false;

            // Variável local (não nullable) para o EF traduzir corretamente o filtro de exclusão na alteração.
            var idParaIgnorar = ignorarVeiculoId.GetValueOrDefault();

            return await _dataset.AsNoTracking()
                .AnyAsync(v =>
                    v.Placa != null
                    && (idParaIgnorar <= 0 || v.Id != idParaIgnorar)
                    && (v.Placa == placaNorm
                        || v.Placa.Replace("-", "").Replace(" ", "").ToUpper() == placaNorm));
        }
    }
}
