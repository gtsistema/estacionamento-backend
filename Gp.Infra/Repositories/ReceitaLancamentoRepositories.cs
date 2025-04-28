using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Extensions;
using Gp.Domain.Input.Receita;
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
    public class ReceitaLancamentoRepositories : BaseRepositories<ReceitaLancamento>, IReceitaLancamentoRepositories
    {
        private DbSet<ReceitaLancamento> _dataset;
        private readonly IMapper _mapper;

        public ReceitaLancamentoRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<ReceitaLancamento>();
            _mapper = mapper;
        }

        public async Task<Receita> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno)
        {
            return await _context.Receita
                    .Where(p => p.Descricao.Contains(descricao)
                    && p.MesReferente == mesDoAno)
                    .FirstOrDefaultAsync();
        }
    }
}
