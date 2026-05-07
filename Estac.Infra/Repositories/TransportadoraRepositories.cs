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
using System.Linq;

namespace Estac.Infra.Repositories
{
    public class TransportadoraRepositories : BaseRepositoriesNone<Transportadora>, ITransportadoraRepositories
    {
        private DbSet<Transportadora> _dataset;
        private readonly IMapper _mapper;
       

        public TransportadoraRepositories(
            GtsContext context,
            IMapper _mapper ) : base(context)
        {
            this._mapper = _mapper;
            _dataset = context.Set<Transportadora>();
        }

        /// <summary>
        /// O <see cref="BaseRepositoriesNone{T}.Alterar(T)"/> só aplica <c>SetValues</c> em <c>Transportadora</c>;
        /// a tabela <c>Pessoa</c> não é atualizada sem incluir e copiar os escalares explicitamente.
        /// </summary>
        public override async Task<Transportadora> Alterar(Transportadora item)
        {
            try
            {
                var result = await _dataset
                    .Include(x => x.Pessoa)
                    .SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    return null;

                item.PessoaId = result.PessoaId;
                _context.Entry(result).CurrentValues.SetValues(item);

                if (item.Pessoa != null && result.Pessoa != null)
                {
                    var destino = result.Pessoa;
                    var origem = item.Pessoa;
                    var dataCriacao = destino.DataCriacao;

                    destino.TipoPessoa = origem.TipoPessoa;
                    destino.NomeRazaoSocial = origem.NomeRazaoSocial;
                    destino.Documento = origem.Documento;
                    destino.Ativo = origem.Ativo;
                    destino.Descricao = origem.Descricao;
                    destino.DataCriacao = dataCriacao;
                    destino.DataAtualizacao = DateTime.Now;
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task<Transportadora> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<TransportadoraSearchOutput>> Paginar(TransportadoraFilterInput input)
        {
            var cnpjFiltro = string.IsNullOrWhiteSpace(input.Cnpj) ? null : input.Cnpj.SomenteDigitos().ToLower();

            var result = await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Contatos)
                        .Where(x => (string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower())) &&
                                    (string.IsNullOrEmpty(input.RazaoSocial) || x.Pessoa.NomeRazaoSocial.ToLower().Contains(input.RazaoSocial.ToLower())) &&
                                    (string.IsNullOrEmpty(input.DescricaoPessoa) || x.Pessoa.Descricao.ToLower().Contains(input.DescricaoPessoa.ToLower())) &&
                                    (cnpjFiltro == null || (x.Pessoa.Documento != null && x.Pessoa.Documento.ToLower().Contains(cnpjFiltro))) &&
                                    (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.Pessoa.DataCriacao.Date <= input.DataInicial && x.Pessoa.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                        .Select(x => new TransportadoraSearchOutput 
                        {
                            Id = x.Id,  
                            PessoaId = x.PessoaId,
                            Fantasia = x.Pessoa.Descricao,
                            RazaoSocial = x.Pessoa.NomeRazaoSocial,
                            Cnpj = x.Pessoa.Documento,
                            Email = x.Pessoa.Contatos
                                .Where(c => c.Email != null && c.Email != "")
                                .OrderByDescending(c => c.Principal)
                                .Select(c => c.Email)
                                .FirstOrDefault(),
                            Contato = x.Pessoa.Contatos.Where(c => c.Principal).Select(c => c.Telefone ?? c.Email ?? c.Cpf).FirstOrDefault(),
                            ativo = x.Pessoa.Ativo,
                            ResponsavelLegal = x.ResponsavelLegal,
                            ResponsavelCpf = x.ResponsavelCpf,
                            ResponsavelEmail = x.ResponsavelEmail,
                            ResponsavelTelefone = x.ResponsavelTelefone
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            foreach (var item in result.Results)
            {
                item.Cnpj = item.Cnpj.FormatarCnpj();
                item.ResponsavelCpf = item.ResponsavelCpf.FormatarCpf();
            }

            return result;
        }
        public async Task<Transportadora> SelecionarIdSimplificado(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransportadoraPorCnpjOutput> SelecionarPorCnpj(string cnpj)
        {
            var cnpjNormalizado = cnpj.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(cnpjNormalizado))
                return null;

            return await _dataset
                .AsNoTracking()
                .Where(x => x.Pessoa != null
                    && x.Pessoa.Documento != null
                    && x.Pessoa.Documento == cnpjNormalizado)
                .Select(x => new TransportadoraPorCnpjOutput
                {
                    Id = x.Id,
                    Cnpj = x.Pessoa.Documento,
                    RazaoSocial = x.Pessoa.NomeRazaoSocial,
                    NomeFantasia = x.Pessoa.Descricao,
                    NomeResponsavel = x.ResponsavelLegal,
                    CpfResponsavel = x.ResponsavelCpf.FormatarCpf(),
                    TelefoneResponsavel = x.ResponsavelTelefone.FormatarTelefone()
                })
                .FirstOrDefaultAsync();
        }
    }
}