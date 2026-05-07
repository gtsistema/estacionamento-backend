using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Output.Transportadora;
using Estac.Domain.Shared;
using Estac.Infra.Context;
using Estac.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Data;

namespace Estac.Infra.Repositories
{
    public class MotoristaRepositories : BaseRepositoriesNone<Motorista>, IMotoristaRepositories
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
            var termoBusca = string.IsNullOrWhiteSpace(input.Descricao) ? null : input.Descricao.Trim().ToLower();
            var cpf = string.IsNullOrWhiteSpace(input.Cpf) ? null : input.Cpf.SomenteDigitos().ToLower();

            var result = await _dataset
                        .AsNoTracking()
                        .Where(x =>
                            (!input.TransportadoraId.HasValue
                                || x.VeiculoMotoristas.Any(vm => vm.Veiculo != null
                                    && vm.Veiculo.TransportadoraId == input.TransportadoraId.Value))
                            && (termoBusca == null
                                || (x.Descricao != null && x.Descricao.ToLower().Contains(termoBusca))
                                || (x.Pessoa.NomeRazaoSocial != null && x.Pessoa.NomeRazaoSocial.ToLower().Contains(termoBusca))
                                || (x.Pessoa.Descricao != null && x.Pessoa.Descricao.ToLower().Contains(termoBusca))
                                || (x.Pessoa.Documento != null && x.Pessoa.Documento.ToLower().Contains(termoBusca)))
                            && (cpf == null|| (x.Pessoa.Documento != null && x.Pessoa.Documento.ToLower().Contains(cpf))))
                        .OrderBy(o => o.Descricao).ThenBy(t => t.Pessoa.DataCriacao)
                        .Select(x => new MotoristaSearchOutput 
                        {
                            Id = x.Id,  
                            PessoaId = x.PessoaId,
                            Descricao = x.Descricao ?? x.Pessoa.Descricao,
                            CNH = x.CNH,
                            ValidadeCNH = x.ValidadeCNH,
                            DataCriacao = x.Pessoa.DataCriacao,
                            DataAtualizacao = x.Pessoa.DataAtualizacao,
                            Cpf = x.Pessoa.Documento
                        })
                        .GetPaged(input.NumeroPagina, input.TamanhoPagina);

            foreach (var item in result.Results)
                item.Cpf = item.Cpf.FormatarCpf();

            return result;
        }

        public async Task<Motorista> SelecionarPorIdCompleto(int id)
        {
            return await _dataset
                        .AsNoTracking()
                        .Include(x => x.Pessoa.Enderecos)
                        .Include(x => x.Pessoa.Contatos)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MotoristaPorCpfOutput> SelecionarPorCpf(string cpf)
        {
            var cpfNormalizado = cpf.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(cpfNormalizado))
                return null;

            return await _dataset
                .AsNoTracking()
                .Where(x => x.Pessoa != null
                    && x.Pessoa.Documento != null
                    && x.Pessoa.Documento == cpfNormalizado)
                .Select(x => new MotoristaPorCpfOutput
                {
                    Cpf = x.Pessoa.Documento.FormatarCpf(),
                    Nome = x.Pessoa.NomeRazaoSocial
                })
                .FirstOrDefaultAsync();
        }

    }
}
