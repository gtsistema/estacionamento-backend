using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.EntityFrameworkCore;
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
                                    string.IsNullOrEmpty(input.RazaoSocial) || x.Pessoa.NomeRazaoSocial.ToLower().Contains(input.RazaoSocial.ToLower()) &&
                                    string.IsNullOrEmpty(input.Fantasia) || x.Pessoa.NomeRazaoSocial.ToLower().Contains(input.Fantasia.ToLower()) &&
                                    string.IsNullOrEmpty(input.Cnpj) || x.Pessoa.Documento.ToLower().Contains(input.Cnpj.ToLower()) &&
                                    (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.Pessoa.DataCriacao.Date <= input.DataInicial && x.Pessoa.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                        .Select(x => new TransportadoraSearchOutput 
                        {
                            Id = x.Id,  
                            Descricao = x.Descricao,
                            PessoaId = x.PessoaId,
                            Fantasia = x.Pessoa.NomeFantasia,
                            RazaoSocial = x.Pessoa.NomeRazaoSocial
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            return result;
        }
    }
}