using AutoMapper;
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

        public TransportadoraService(IErrorServices _errorServices,
                               ITransportadoraRepositories repositories, IMapper mapper,
                               IPessoaContatoRepositories contatoRepositories,
            IPessoaEnderecoRepositories enderecoRepositories,
               IUnitOfWork unitOfWork) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
            _contatoRepositories = contatoRepositories;
            _enderecoRepositories = enderecoRepositories;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var transportadora = _mapper.Map<Transportadora>(input);

                transportadora.Id = input.Id;
                transportadora.Descricao = input.PessoaJuridica.Descricao;

                await _contatoRepositories.AtualizarContatos(transportadora.Pessoa.Id, transportadora.Pessoa.Contatos);
                await _enderecoRepositories.AtualizarEndereco(transportadora.Pessoa.Id, transportadora.Pessoa.Enderecos);

                ValoresPadrao(transportadora);
                transportadora.Pessoa.Contatos = null;
                transportadora.Pessoa.Enderecos = null;

                await _repositories.Alterar(transportadora);

                await _unitOfWork.SaveChangesAsync();
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
            var result = await _repositories.Existe(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var despesa = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

        private static void ValoresPadrao(Transportadora result)
        {
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Juridica);
            result.Pessoa.AdicionarPapel(TipoPapel.Tranportadora);
            result.Descricao = result.Pessoa.Descricao;
            result.PessoaId = result.Pessoa.Id;
           
        }
    }
}
