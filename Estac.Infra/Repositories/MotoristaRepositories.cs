using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class MotoristaRepositories : BaseRepositoriesNone<Motorista>, IMotoristaRepositories
    {
        private DbSet<Motorista> _dataset;
        private readonly IMapper _mapper;

        public MotoristaRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Motorista>();
        }

        public async Task<PagedResult<MotoristaSearchOutput>> Paginar(MotoristaFilterInput input)
        {
            var termoBusca = string.IsNullOrWhiteSpace(input.Descricao) ? null : input.Descricao.Trim().ToLower();
            var cpf = string.IsNullOrWhiteSpace(input.Cpf) ? null : input.Cpf.Trim().ToLower();

            var result = await _dataset
                        .AsNoTracking()
                        .Where(x =>
                            (!input.TransportadoraId.HasValue
                                || (x.Veiculo != null && x.Veiculo.TransportadoraId == input.TransportadoraId.Value))
                            && (termoBusca == null
                                || (x.Descricao != null && x.Descricao.ToLower().Contains(termoBusca))
                                || (x.Pessoa.NomeRazaoSocial != null && x.Pessoa.NomeRazaoSocial.ToLower().Contains(termoBusca))
                                || (x.Pessoa.NomeFantasia != null && x.Pessoa.NomeFantasia.ToLower().Contains(termoBusca))
                                || (x.Pessoa.Documento != null && x.Pessoa.Documento.ToLower().Contains(termoBusca)))
                            && (cpf == null|| (x.Pessoa.Documento != null && x.Pessoa.Documento.ToLower().Contains(cpf))))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                        .Select(x => new MotoristaSearchOutput 
                        {
                            Id = x.Id,  
                            PessoaId = x.PessoaId,
                            Descricao = x.Descricao ?? x.Pessoa.NomeFantasia,
                            CNH = x.CNH,
                            ValidadeCNH = x.ValidadeCNH,
                            DataCriacao = x.Pessoa.DataCriacao,
                            DataAtualizacao = x.Pessoa.DataAtualizacao,
                            Cpf = x.Pessoa.Documento
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }

        public async Task<MotoristaVinculosPorPlacaOutput> ObterVinculosPorPlaca(string placa)
        {
            var placaNorm = (placa ?? string.Empty).Trim().ToUpperInvariant();
            if (string.IsNullOrEmpty(placaNorm))
                return null;

            var veiculo = await _context.Veiculo
                .AsNoTracking()
                .Include(v => v.Motorista)!.ThenInclude(m => m.Pessoa)
                .Include(v => v.Transportadora)!.ThenInclude(t => t.Pessoa)
                .FirstOrDefaultAsync(v => v.Placa != null && v.Placa.ToUpper() == placaNorm);

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
