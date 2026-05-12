using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Motorista;
using Estac.Domain.Validators;
using Estac.Service.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class MotoristaService : ServiceResult<MotoristaOutput>, IMotoristaService
    {
        private readonly IMotoristaRepositories _repositories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPessoaContatoRepositories _contatoRepositories;
        private readonly IPessoaEnderecoRepositories _enderecoRepositories;
        private readonly IPessoaRepositories _pessoaRepositories;
        private readonly IVeiculoRepositories _veiculoRepositories;

        public MotoristaService(IErrorServices _errorServices,
                               IMotoristaRepositories repositories, IMapper mapper,
                               IUnitOfWork unitOfWork,
                               IPessoaContatoRepositories contatoRepositories,
                               IPessoaEnderecoRepositories enderecoRepositories,
                               IPessoaRepositories pessoaRepositories,
                               IVeiculoRepositories veiculoRepositories) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contatoRepositories = contatoRepositories;
            _enderecoRepositories = enderecoRepositories;
            _pessoaRepositories = pessoaRepositories;
            _veiculoRepositories = veiculoRepositories;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.SelecionarPorIdCompleto(id);

            return await RetornOk(_mapper.Map<MotoristaOutput>(result));
        }

        public async Task<ActionResult> Buscar(MotoristaFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            return await RetornOk(result);
        }

        public async Task<ActionResult> BuscarPorCpf(string cpf)
        {
            var cpfNormalizado = cpf.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(cpfNormalizado))
                return await RetornNo(false, "CPF inválido.");

            var result = await _repositories.SelecionarPorCpf(cpfNormalizado);
            if (result == null)
                return await RetornNo(false, "Motorista não localizado na base de dados.", statusCode: 404);

            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(MotoristaPostInput input)
        {
            var contatosInvalidos = ValidarContatos(input?.PessoaFisica?.Contatos);
            if (contatosInvalidos.Count > 0)
                return await RetornNo(new { }, contatosInvalidos);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var motorista = _mapper.Map<Motorista>(input);

                ValoresPadrao(motorista);

                await _repositories.Gravar(motorista);

                await _unitOfWork.CommitAsync();

                return await RetornOk(motorista);
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(false, ex.Message);
            }
        }

        private static void ValoresPadrao(Motorista motorista)
        {
            motorista.Descricao = motorista.Pessoa.Descricao;
            motorista.PessoaId = motorista.Pessoa.Id;
            motorista.Pessoa.AdicionarTipoPessoa(TipoPessoa.Fisica);
            motorista.Pessoa.AdicionarPapel(TipoPapel.Motorista);
            motorista.Pessoa.Contatos = null;
            motorista.Pessoa.Enderecos = null;
        }

        public async Task<ActionResult> Alterar(MotoristaPutInput input)
        {
            var contatosInvalidos = ValidarContatos(input?.PessoaFisica?.Contatos);
            if (contatosInvalidos.Count > 0)
                return await RetornNo(new { }, contatosInvalidos);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var motorista = _mapper.Map<Motorista>(input);

                await _contatoRepositories.AtualizarContatos(motorista.Pessoa.Id, motorista.Pessoa.Contatos);
                await _enderecoRepositories.AtualizarEndereco(motorista.Pessoa.Id, motorista.Pessoa.Enderecos);

                ValoresPadrao(motorista);

                var motoristaAlterado = await _repositories.Alterar(motorista);

                await _unitOfWork.CommitAsync();

                return await RetornOk(motoristaAlterado);
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackAsync();

                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {

                if (!await _repositories.Existe(id))
                    return await RetornNo(false, "Motorista não localizado na base de dados.", statusCode: 404);

                if (await _veiculoRepositories.PossuiVeiculoMotoristaNaTransportadoraAsync(id))
                    throw new InvalidOperationException(
                        "Não é possível excluir: existem motoristas vinculados a veículos desta transportadora.");

                if (await _repositories.PossuiEntradaSaidaVinculadaAsync(id))
                    return await RetornNo(false, "Não é possível excluir: existem registros de entrada/saída vinculados a este motorista.");

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

        private static List<ValidationFailure> ValidarContatos(IEnumerable<Domain.Input.PessoaContato.PessoaContatoInput> contatos)
        {
            if (contatos == null)
                return new List<ValidationFailure>();

            var validator = new PessoaContatoInputValidator();
            var erros = new List<ValidationFailure>();

            foreach (var contato in contatos)
            {
                var result = validator.Validate(contato);
                if (!result.IsValid)
                    erros.AddRange(result.Errors);
            }

            return erros;
        }
    }
}
