using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Estacionamento;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class EstacionamentoService : ServiceResult<EstacionamentoOutput>, IEstacionamentoService
    {
        private readonly IEstacionamentoRepositories _repositories;
        private readonly IMapper _mapper;

        public EstacionamentoService(IErrorServices _errorServices,
                               IEstacionamentoRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.SelecionarPorIdCompleto(id);

                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                return await RetornOk(_mapper.Map<EstacionamentoOutput>(result));
            }
            catch(Exception ex) 
            {   
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Buscar(EstacionamentoFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            var mapper = _mapper.Map<IEnumerable<EstacionamentoOutput>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> Gravar(EstacionamentoPostInput input)
        {
            try
            {
                //var validations = EstacionamentoPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Estacionamento>(input);
                ValoresPadrao(result);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(EstacionamentoPutInput input)
        {
            try
            {
                //var validations = EstacionamentoPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Estacionamento>(input);
                ValoresPadrao(result);

                await _repositories.Alterar(result);

                return await RetornOk(await _repositories.Alterar(result));
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

            var estacionamento = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

        private static void ValoresPadrao(Estacionamento result)
        {
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Juridica);
            result.Pessoa.AdicionarPapel(TipoPapel.Estacionamento);
        }
    }
}
