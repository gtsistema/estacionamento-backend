using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class MotoristaRepositories : BaseRepositories<Motorista>, IMotoristaRepositories
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
            var result = await _dataset
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower()) &&
                                    string.IsNullOrEmpty(input.Nome) || x.Pessoa.Descricao.ToLower().Contains(input.Nome.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.DataCriacao.Date <= input.DataInicial && x.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.DataCriacao)
                        .Select(x => new MotoristaSearchOutput 
                        {
                            Id = x.Id,  
                            Descricao = x.Descricao,
                            PessoaId = x.PessoaId,
                            Nome = x.Pessoa.Descricao,
                            Cnh = x.CNH
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }
    }
}
