using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class EstacionamentoConfiguracaoRepositories : IEstacionamentoConfiguracaoRepositories
    {
        private readonly GtsContext _context;
        private readonly DbSet<EstacionamentoConfiguracao> _dataset;

        public EstacionamentoConfiguracaoRepositories(GtsContext context)
        {
            _context = context;
            _dataset = context.Set<EstacionamentoConfiguracao>();
        }

        public async Task<EstacionamentoConfiguracao> ObterPorEstacionamentoIdAsync(int estacionamentoId)
        {
            return await _dataset.AsNoTracking()
                .FirstOrDefaultAsync(x => x.EstacionamentoId == estacionamentoId);
        }

        public async Task<EstacionamentoConfiguracao> ObterPorIdAsync(int id)
        {
            return await _dataset.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistePorEstacionamentoIdAsync(int estacionamentoId)
        {
            return await _dataset.AsNoTracking()
                .AnyAsync(x => x.EstacionamentoId == estacionamentoId);
        }

        public async Task<EstacionamentoConfiguracao> GravarAsync(EstacionamentoConfiguracao entity)
        {
            entity.DataCriacao = DateTime.UtcNow;
            entity.DataAtualizacao = null;
            await _dataset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EstacionamentoConfiguracao> AlterarAsync(EstacionamentoConfiguracao entity)
        {
            var atual = await _dataset.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (atual == null)
                return null;

            atual.TimeZoneId = entity.TimeZoneId;
            atual.Cultura = entity.Cultura;
            atual.Ativo = entity.Ativo;
            atual.DataAtualizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return atual;
        }
    }
}
