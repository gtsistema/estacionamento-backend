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
        private readonly IVeiculoRepositories _veiculoRepositories;

        public TransportadoraRepositories(
            GtsContext context,
            IMapper _mapper,
            IVeiculoRepositories veiculoRepositories) : base(context)
        {
            this._mapper = _mapper;
            _veiculoRepositories = veiculoRepositories;
            _dataset = context.Set<Transportadora>();
        }

        /// <summary>
        /// Insere transportadora, pessoa e conta bancária (quando informada).
        /// Não chama <c>SaveChanges</c> — o <see cref="IUnitOfWork"/> do service persiste no Commit.
        /// </summary>
        public async Task<Transportadora> GravarCompleto(Transportadora item)
        {
            try
            {
                item.Id = 0;
                item.Veiculos = null;

                if (item.ContasBancarias != null)
                {
                    foreach (var conta in item.ContasBancarias.Where(c => c != null))
                    {
                        conta.Id = 0;
                        conta.Transportadora = null;
                        conta.Estacionamento = null;
                    }
                }

                await _context.AddAsync(item);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        /// <summary>
        /// O <see cref="BaseRepositoriesNone{T}.Alterar(T)"/> só aplica <c>SetValues</c> em <c>Transportadora</c>;
        /// a tabela <c>Pessoa</c> não é atualizada sem incluir e copiar os escalares explicitamente.
        /// Contas em <c>ContaBancaria</c> precisam ser atualizadas ou inseridas explicitamente.
        /// </summary>
        public override async Task<Transportadora> Alterar(Transportadora item)
        {
            try
            {
                var incomingContas = item.ContasBancarias?.Where(c => c != null).ToList();
                item.ContasBancarias = null;

                var result = await _dataset
                    .Include(x => x.Pessoa)
                    .Include(x => x.ContasBancarias)
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

                if (incomingContas != null && incomingContas.Count > 0)
                {
                    var incoming = incomingContas[0];
                    incoming.TransportadoraId = result.Id;

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
                        incoming.TransportadoraId = result.Id;
                        incoming.DataCriacao = dataCriacao;
                        _context.Entry(alvo).CurrentValues.SetValues(incoming);
                    }
                    else
                    {
                        incoming.Id = 0;
                        incoming.TransportadoraId = result.Id;
                        await _context.ContaBancaria.AddAsync(incoming);
                    }
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
                        .Include(x => x.ContasBancarias)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResult<TransportadoraSearchOutput>> Paginar(TransportadoraFilterInput input)
        {
            var cnpjFiltro = string.IsNullOrWhiteSpace(input.Cnpj) ? null : input.Cnpj.SomenteAlfanumericos().ToLower();

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
            return await _dataset.Include(x => x.Pessoa)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransportadoraPorCnpjOutput> SelecionarPorCnpj(string cnpj)
        {
            var cnpjNormalizado = cnpj.SomenteAlfanumericos();
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

        public async Task<bool> ExistePorCnpjAsync(string cnpj)
        {
            var cnpjNormalizado = cnpj.SomenteAlfanumericos();
            if (string.IsNullOrWhiteSpace(cnpjNormalizado))
                return false;

            return await _context.Set<Pessoa>()
                .AsNoTracking()
                .AnyAsync(p => p.Documento == cnpjNormalizado);
        }

        public Task<bool> PossuiVeiculoVinculadoAsync(int transportadoraId) =>
            _context.Set<Veiculo>().AsNoTracking().AnyAsync(v => v.TransportadoraId == transportadoraId);

        public Task<bool> PossuiEntradaSaidaVinculadaAsync(int transportadoraId) =>
            _context.Set<EntradaSaida>().AsNoTracking().AnyAsync(e => e.TransportadoraId == transportadoraId);

        /// <summary>
        /// Exclui transportadora e a <see cref="Pessoa"/> associada.
        /// Contatos, endereços e papéis da pessoa somem em cascata ao apagar <see cref="Pessoa"/> (configuração EF).
        /// A FK Transportadora → Pessoa é <c>Restrict</c>: não basta remover só a transportadora; a pessoa é removida em seguida.
        /// Chamador deve garantir que não existam <see cref="Veiculo"/> nem <see cref="EntradaSaida"/> com FK para esta transportadora (Restrict).
        /// Também falha se existir <see cref="VeiculoMotorista"/> ligando motorista a veículo desta transportadora.
        /// </summary>
        public async Task Remove(int id)
        {
            try
            {
                

                var transportadora = await _dataset
                    .Include(x => x.Pessoa)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (transportadora is null)
                    return;

                _context.Remove(transportadora);

                if (transportadora.Pessoa is not null)
                    _context.Remove(transportadora.Pessoa);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}