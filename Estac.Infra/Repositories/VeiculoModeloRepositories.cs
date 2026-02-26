using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.VeiculoModelo;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class VeiculoModeloRepositories : BaseRepositories<VeiculoModelo>, IVeiculoModeloRepositories
    {
        private DbSet<VeiculoModelo> _dataset;
        private readonly IMapper _mapper;

        public VeiculoModeloRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<VeiculoModelo>();
        }

        public async Task<PagedResult<VeiculoModeloSearchOutput>> Paginar(VeiculoModeloFilterInput input)
        {
            var result = await _dataset
                        .AsNoTracking()
                        .Include(x => x.VeiculoMarca)
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.DataCriacao)
                        .Select(x => new VeiculoModeloSearchOutput 
                        {
                            Id = x.Id,  
                            Descricao = x.Descricao,
                            marcaId = x.VeiculoMarcaId,
                            Marca = x.VeiculoMarca.Descricao
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }
    }
}
