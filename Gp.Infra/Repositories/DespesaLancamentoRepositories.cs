using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Extensions;
using Gp.Domain.Input.Despesa;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;
using Gp.Domain.Shared;
using Gp.Infra.Context;
using Gp.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gp.Infra.Repositories
{
    public class DespesaLancamentoRepositories : BaseRepositories<DespesaLancamento>, IDespesaLancamentoRepositories
    {
        private DbSet<DespesaLancamento> _dataset;
        private readonly IMapper _mapper;

        public DespesaLancamentoRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<DespesaLancamento>();
            _mapper = mapper;
        }

        public async Task<Despesa> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno)
        {
            return await _context.Despesa
                    .Where(p => p.Descricao.Contains(descricao)
                    && p.MesReferente == mesDoAno)
                    .FirstOrDefaultAsync();
        }
    }
}
