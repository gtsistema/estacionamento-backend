using AutoMapper;
using Estac.Domain.Dtos;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Despesa;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class DespesaPagamentoRepositories : BaseRepositories<DespesaPagamento>, IDespesaPagamentoRepositories
    {
        private DbSet<DespesaPagamento> _dataset;
        private readonly IMapper _mapper;

        public DespesaPagamentoRepositories(GpContext context, IMapper mapper) : base(context)
        {
            _dataset = context.Set<DespesaPagamento>();
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
