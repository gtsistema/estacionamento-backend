using AutoMapper;
using Estac.Domain.Dtos;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Receita;
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
    //public class ReceitaLancamentoRepositories : BaseRepositories<ReceitaLancamento>, IReceitaLancamentoRepositories
    //{
    //    private DbSet<ReceitaLancamento> _dataset;
    //    private readonly IMapper _mapper;

    //    public ReceitaLancamentoRepositories(GpContext context, IMapper mapper) : base(context)
    //    {
    //        _dataset = context.Set<ReceitaLancamento>();
    //        _mapper = mapper;
    //    }

    //    public async Task<Receita> GetIdByDescricaoAsync(string descricao, MesDoAno mesDoAno)
    //    {
    //        return await _context.Receita
    //                .Where(p => p.Descricao.Contains(descricao)
    //                && p.MesReferente == mesDoAno)
    //                .FirstOrDefaultAsync();
    //    }
    //}
}
