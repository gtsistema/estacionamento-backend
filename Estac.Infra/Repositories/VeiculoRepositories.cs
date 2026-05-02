using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
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
            var placa = string.IsNullOrWhiteSpace(input.Placa) ? null : input.Placa.Trim().ToLower();

            var result = await _dataset
                        .AsNoTracking()
                        .Where(x =>
                            (!input.TransportadoraId.HasValue
                                || (x.TransportadoraId != null && x.TransportadoraId == input.TransportadoraId.Value))
                            && (termoBusca == null
                                || (x.Descricao != null && x.Descricao.ToLower().Contains(termoBusca))
                                || (x.Placa != null && x.Placa.ToLower().Contains(termoBusca))
                                || (x.VeiculoModelo != null && x.VeiculoModelo.Descricao != null
                                    && x.VeiculoModelo.Descricao.ToLower().Contains(termoBusca)))
                            && (placa == null || (x.Placa != null && x.Placa.ToLower().Contains(placa))))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.DataCriacao)
                        .Select(x => new VeiculoSearchOutput 
                        {
                            Ano = x.Ano,
                            Ativo = x.Ativo,
                            Cor = x.Cor,
                            Id = x.Id,
                            Placa = x.Placa,
                            ModeloMarca = x.VeiculoModelo.Descricao + " - " + x.VeiculoModelo.VeiculoMarca.Descricao
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
                        .Include(x => x.Motorista.Pessoa)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
