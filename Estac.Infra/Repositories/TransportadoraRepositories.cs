using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class TransportadoraRepositories : BaseRepositoriesNone<Transportadora>, ITransportadoraRepositories
    {
        private DbSet<Transportadora> _dataset;
        private readonly IMapper _mapper;

        public TransportadoraRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Transportadora>();
        }

        public async Task<PagedResult<TransportadoraSearchOutput>> Paginar(TransportadoraFilterInput input)
        {
            var result = await _dataset
                        .AsNoTracking()
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower()) &&
                                    string.IsNullOrEmpty(input.Nome) || x.Pessoa.Descricao.ToLower().Contains(input.Nome.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.Pessoa.DataCriacao.Date <= input.DataInicial && x.Pessoa.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                        .Select(x => new TransportadoraSearchOutput 
                        {
                            Id = x.Id,  
                            Descricao = x.Descricao,
                            PessoaId = x.PessoaId,
                            Nome = x.Pessoa.Descricao,
                            CNH = x.CNH
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }
    }
}
