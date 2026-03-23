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
                        .Include(x => x.ContasBancarias)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<EstacionamentoSearchOutput>> Paginar(EstacionamentoFilterInput input)
        {
            return await _dataset
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
                             Descricao = x.Descricao,
                             PessoaId = x.PessoaId,
                             Documento = x.Pessoa.Documento,
                             Ativo = x.Pessoa.Ativo,
                             NomeFantasia = x.Pessoa.NomeFantasia,
                             NomeRazaoSocial = x.Pessoa.NomeRazaoSocial,
                             Email = x.Pessoa.Email,
                             Tipo = x.Pessoa.TipoPessoa,
                         })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

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
    }
}
