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
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;

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

        /// <summary>
        /// O <see cref="BaseRepositoriesNone{T}.Alterar(T)"/> só aplica <c>SetValues</c> em <c>Estacionamento</c>;
        /// contas em <c>ContaBancaria</c> precisam ser atualizadas ou inseridas explicitamente.
        /// </summary>
        public override async Task<Estacionamento> Alterar(Estacionamento item)
        {
            try
            {
                var incomingContas = item.ContasBancarias?.Where(c => c != null).ToList();

                item.ContasBancarias = null;

                var result = await _dataset
                    .Include(x => x.ContasBancarias)
                    .SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    return null;

                item.PessoaId = result.PessoaId;

                _context.Entry(result).CurrentValues.SetValues(item);

                if (incomingContas != null && incomingContas.Count > 0)
                {
                    var incoming = incomingContas[0];
                    incoming.EstacionamentoId = result.Id;

                    var existentes = result.ContasBancarias?.ToList() ?? new List<ContaBancaria>();

                    ContaBancaria alvo = null;
                    if (incoming.Id > 0)
                        alvo = existentes.FirstOrDefault(c => c.Id == incoming.Id);
                    else if (existentes.Count > 0)
                        alvo = existentes[0];

                    if (alvo != null)
                    {
                        var dataCriacao = alvo.DataCriacao;
                        incoming.Id = alvo.Id;
                        incoming.EstacionamentoId = result.Id;
                        incoming.DataCriacao = dataCriacao;
                        _context.Entry(alvo).CurrentValues.SetValues(incoming);
                    }
                    else
                    {
                        incoming.Id = 0;
                        incoming.EstacionamentoId = result.Id;
                        await _context.ContaBancaria.AddAsync(incoming);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task<Estacionamento> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .Include(x => x.ContasBancarias)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<EstacionamentoSearchOutput>> Paginar(EstacionamentoFilterInput input)
        {
            var result = await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .Where(x => string.IsNullOrEmpty(input.Descricao) || x.Descricao.ToLower().Contains(input.Descricao.ToLower()) ||
                                    string.IsNullOrEmpty(input.Descricao) || x.Pessoa.Documento.ToLower().Contains(input.Descricao.ToLower()) 
                               && (!input.DataInicial.HasValue && !input.DataFinal.HasValue || x.Pessoa.DataCriacao.Date <= input.DataInicial && x.Pessoa.DataCriacao.Date >= input.DataFinal))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                         .Select(x => new EstacionamentoSearchOutput
                         {
                             Id = x.Id,
                             NomeFantasia = x.Descricao,
                             PessoaId = x.PessoaId,
                             Cnpj = x.Pessoa.Documento,
                             Ativo = x.Pessoa.Ativo,
                             DescricaoPessoa = x.Pessoa.Descricao,
                             NomeRazaoSocial = x.Pessoa.NomeRazaoSocial,
                             ResponsavelLegal = x.ResponsavelLegal,
                             ResponsavelCpf = x.ResponsavelCpf,
                             ResponsavelEmail = x.ResponsavelEmail,
                             ResponsavelTelefone = x.ResponsavelTelefone,
                         })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            foreach (var item in result.Results)
            {
                item.Cnpj = item.Cnpj.FormatarCnpj();
                item.ResponsavelCpf = item.ResponsavelCpf.FormatarCpf();
            }

            return result;
        }

        public async Task<IEnumerable<MenuFotoOutput>> ListarFotosPorEstacionamentoAsNoTracking(int estacionamentoId)
        {
            return await _context.EstacionamentoFoto
                .AsNoTracking()
                .Where(x => x.EstacionamentoId == estacionamentoId)
                .Select(x => new MenuFotoOutput
                {
                    Id = x.Id,
                    NomeArquivo = x.Descricao,
                    EhPrincipal = x.Principal,
                    ContentType = x.ContentType,
                    FotoBase64 = Convert.ToBase64String(x.Foto)
                })
                .ToListAsync();

        }

        public async Task<IEnumerable<EstacionamentoFoto>> ListarFotosPorEstacionamento(int id)
        {
           return await _context.EstacionamentoFoto
                .Where(x => x.EstacionamentoId == id).ToListAsync();
        }

        public async Task UploadFotos(List<EstacionamentoFoto> fotos)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.EstacionamentoFoto.AddRangeAsync(fotos);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ExcluirFotos(int id)
        {
              await _context.EstacionamentoFoto
                 .Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Estacionamento> SelecionarPorDescricao(string descricao)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .SingleOrDefaultAsync(x => x.Descricao == descricao);
        }
    }
}
