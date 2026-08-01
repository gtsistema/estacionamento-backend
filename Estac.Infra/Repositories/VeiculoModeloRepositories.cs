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

        public async Task<int?> ObterIdPorDescricaoExataAsync(string descricao, int? veiculoMarcaId = null)
        {
            var termo = descricao?.Trim();
            if (string.IsNullOrWhiteSpace(termo))
                return null;

            var termoLower = termo.ToLower();

            var query = _dataset.AsNoTracking()
                .Where(x => x.Descricao != null && x.Descricao.Trim().ToLower() == termoLower);

            if (veiculoMarcaId.HasValue && veiculoMarcaId.Value > 0)
                query = query.Where(x => x.VeiculoMarcaId == veiculoMarcaId.Value);

            return await query
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> ObterMarcaIdPorDescricaoExataAsync(string descricao)
        {
            var termo = descricao?.Trim();
            if (string.IsNullOrWhiteSpace(termo))
                return null;

            var termoLower = termo.ToLower();

            return await _context.Set<VeiculoMarca>()
                .AsNoTracking()
                .Where(x => x.Descricao != null && x.Descricao.Trim().ToLower() == termoLower)
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> ObterOuCriarMarcaIdPorDescricaoAsync(string descricao)
        {
            var existente = await ObterMarcaIdPorDescricaoExataAsync(descricao);
            if (existente.HasValue)
                return existente.Value;

            var marca = new VeiculoMarca
            {
                Descricao = descricao.Trim()
            };

            await _context.Set<VeiculoMarca>().AddAsync(marca);
            await _context.SaveChangesAsync();
            return marca.Id;
        }

        public async Task<int> ObterOuCriarModeloIdPorDescricaoAsync(string descricao, int veiculoMarcaId)
        {
            var existente = await ObterIdPorDescricaoExataAsync(descricao, veiculoMarcaId);
            if (existente.HasValue)
                return existente.Value;

            var modelo = new VeiculoModelo
            {
                Descricao = descricao.Trim(),
                VeiculoMarcaId = veiculoMarcaId,
                DataCriacao = DateTime.UtcNow
            };

            await Gravar(modelo);
            return modelo.Id;
        }
    }
}
