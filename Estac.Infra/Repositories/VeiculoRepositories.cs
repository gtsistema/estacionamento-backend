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
            var result = await _dataset
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Search) || x.Descricao.ToLower().Contains(input.Search.ToLower()) &&
                                    string.IsNullOrEmpty(input.Placa) || x.Placa.ToLower().Contains(input.Placa.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.DataCriacao)
                        .Select(x => new VeiculoSearchOutput 
                        {
                            Ano = x.Ano,
                            Ativo = x.Ativo,
                            Cor = x.Cor,
                            Id = x.Id,
                            Descricao = x.Descricao,
                            Placa = x.Placa
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }
    }
}
