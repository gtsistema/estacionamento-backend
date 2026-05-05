using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Motorista;
using Estac.Infra.Repositories;
using Estac.Service.Extensions;
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
        private readonly IVeiculoRepositories _veiculoRepositories;

        public MotoristaService(IErrorServices _errorServices,
                               IMotoristaRepositories repositories, IMapper mapper,
                               IUnitOfWork unitOfWork,
                               IPessoaContatoRepositories contatoRepositories,
                               IPessoaEnderecoRepositories enderecoRepositories,
                               IVeiculoRepositories veiculoRepositories) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contatoRepositories = contatoRepositories;
            _enderecoRepositories = enderecoRepositories;
            _veiculoRepositories = veiculoRepositories;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.Selecionar(id);

            return await RetornOk(_mapper.Map<MotoristaOutput>(result));
        }

        public async Task<ActionResult> Buscar(MotoristaFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(MotoristaPostInput input)
        {
            try
            {

                //var validations = MotoristaPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var motorista = _mapper.Map<Motorista>(input);

                
                ValoresPadrao(motorista);

                await _repositories.Gravar(motorista);

                return await RetornOk(motorista);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        private static void ValoresPadrao(Motorista result)
        {
            result.Descricao = result.Pessoa.Descricao;
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Fisica);
            result.Pessoa.AdicionarPapel(TipoPapel.Estacionamento);
            result.Pessoa.Contatos = null;
            result.Pessoa.Enderecos = null;
        }

        public async Task<ActionResult> Alterar(MotoristaPutInput input)
        {
            try
            {
                //var validations = MotoristaPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var motorista = _mapper.Map<Motorista>(input);

                await _contatoRepositories.AtualizarContatos(motorista.Pessoa.Id, motorista.Pessoa.Contatos);
                await _enderecoRepositories.AtualizarEndereco(motorista.Pessoa.Id, motorista.Pessoa.Enderecos);

                ValoresPadrao(motorista);

                await _repositories.Alterar(motorista);

                return await RetornOk(await _repositories.Alterar(motorista));
            }
            catch (Exception ex) 
            {
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
    }
}
