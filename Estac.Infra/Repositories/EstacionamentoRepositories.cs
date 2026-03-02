using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Estacionamento;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class EstacionamentoRepositories : BaseRepositoriesNone<Estacionamento>, IEstacionamentoRepositories
    {
        private DbSet<Estacionamento> _dataset;
        private readonly IMapper _mapper;

        public EstacionamentoRepositories(GtsContext context, IMapper _mapper) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Estacionamento>();
        }

        public async Task<Estacionamento> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<EstacionamentoSearchOutput>> Paginar(EstacionamentoFilterInput input)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower())
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.Pessoa.DataCriacao.Date <= input.DataInicial && x.Pessoa.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                         .Select(x => new EstacionamentoSearchOutput
                         {
                             Id = x.Id,
                             Descricao = x.Descricao,
                             PessoaId = x.PessoaId,
                             Documento = x.Pessoa.Documento,
                             Ativo = x.Pessoa.Ativo,
                             NomeFantasia = x.Pessoa.NomeFantasia,
                             NomeRazaoSocial = x.Pessoa.NomeRazaoSocial,
                             Email = x.Pessoa.Email,
                             Tipo = x.Pessoa.TipoPessoa
                         })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

        }
    }
}
