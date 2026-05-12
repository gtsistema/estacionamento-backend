using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Transportadora;
using Estac.Infra.Repositories;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class TransportadoraService : ServiceResult<TransportadoraOutput>, ITransportadoraService
    {
        private readonly ITransportadoraRepositories _repositories;
        private readonly IMapper _mapper;
        private readonly IPessoaContatoRepositories _contatoRepositories;
        private readonly IPessoaEnderecoRepositories _enderecoRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVeiculoRepositories _veiculoRepositories;
        public TransportadoraService(IErrorServices _errorServices,
                               ITransportadoraRepositories repositories, IMapper mapper,
                               IPessoaContatoRepositories contatoRepositories,
            IPessoaEnderecoRepositories enderecoRepositories,
               IUnitOfWork unitOfWork,
               IVeiculoRepositories veiculoRepositories) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
            _contatoRepositories = contatoRepositories;
            _enderecoRepositories = enderecoRepositories;
            _unitOfWork = unitOfWork;
            _veiculoRepositories = veiculoRepositories;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.SelecionarPorIdCompleto(id);

                return await RetornOk(_mapper.Map<TransportadoraOutput>(result));
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Buscar(TransportadoraFilterInput filter)
        {
            try
            {
                var result = await _repositories.Paginar(filter);

                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> BuscarPorCnpj(string cnpj)
        {
            try
            {
                var cnpjNormalizado = cnpj.SomenteDigitos();

                if (string.IsNullOrWhiteSpace(cnpjNormalizado))
                    return await RetornNo(false, "CNPJ inválido.");

                var result = await _repositories.SelecionarPorCnpj(cnpjNormalizado);

                if (result == null)
                    return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Gravar(TransportadoraPostInput input)
        {
            try
            {
                var validations = TransportadoraPostInput.Validar(input);

                if (!validations.IsValid)
                    return await RetornNo(new { }, validations.Errors);

                var result = _mapper.Map<Transportadora>(input);
                ValoresPadrao(result);

                await _repositories.Gravar(result);

                return await RetornOk(_repositories.SelecionarPorIdCompleto(result.Id));
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }

        }

        public async Task<ActionResult> Alterar(TransportadoraPutInput input)
        {
            var validations = TransportadoraPutInput.Validar(input);

            if (!validations.IsValid)
                return await RetornNo(new { }, validations.Errors);

            var existente = await _repositories.SelecionarIdSimplificado(input.Id);
            if (existente == null)
                return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var transportadora = _mapper.Map<Transportadora>(input);

                transportadora.Id = input.Id;
                transportadora.Descricao = input.PessoaJuridica.Descricao;
                transportadora.PessoaId = existente.PessoaId;
                transportadora.Pessoa.Id = existente.PessoaId;

                await _contatoRepositories.AtualizarContatos(transportadora.Pessoa.Id, transportadora.Pessoa.Contatos);
                await _enderecoRepositories.AtualizarEndereco(transportadora.Pessoa.Id, transportadora.Pessoa.Enderecos);

                ValoresPadrao(transportadora);

                await _repositories.Alterar(transportadora);

                await _unitOfWork.CommitAsync();

                return await RetornOk(_repositories.SelecionarPorIdCompleto(transportadora.Id));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {

            try
            {
                if (!await _repositories.Existe(id))
                    return await RetornNo(false, "Transportadora não localizada na base de dados.", statusCode: 404);

                if (await _repositories.PossuiVeiculoVinculadoAsync(id))
                    return await RetornNo(false, "Não é possível excluir: existem veículos vinculados a esta transportadora.");

                if (await _repositories.PossuiEntradaSaidaVinculadaAsync(id))
                    return await RetornNo(false, "Não é possível excluir: existem registros de entrada/saída vinculados a esta transportadora.");

                if (await _veiculoRepositories.PossuiVeiculoMotoristaNaTransportadoraAsync(id))
                    return await RetornNo(false, "Não é possível excluir: existem motoristas vinculados a veículos desta transportadora.");

                await _unitOfWork.BeginTransactionAsync();

                await _repositories.Remove(id);

                await _unitOfWork.CommitAsync();

                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        private static void ValoresPadrao(Transportadora result)
        {
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Juridica);
            result.Pessoa.AdicionarPapel(TipoPapel.Tranportadora);
            result.Descricao = result.Pessoa.Descricao;
            result.PessoaId = result.Pessoa.Id;
            result.Pessoa.Contatos = null;
            result.Pessoa.Enderecos = null;
        }
    }
}
