using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Estac.Domain.Input.Transportadora;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Transportadora;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class TransportadoraService : ServiceResult<TransportadoraOutput>, ITransportadoraService
    {
        private readonly ITransportadoraRepositories _repositories;
        private readonly IMapper _mapper;

        public TransportadoraService(IErrorServices _errorServices,
                               ITransportadoraRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.Selecionar(id);

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

                var mapper = _mapper.Map<IEnumerable<TransportadoraOutput>>(result.Results);

                return await RetornOk(mapper);
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
                //var validations = TransportadoraPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Transportadora>(input);
                ValoresPadrao(result);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(TransportadoraPutInput input)
        {
            try
            {
                //var validations = TransportadoraPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Transportadora>(input);
                await _repositories.Alterar(result);
                ValoresPadrao(result);

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

            var despesa = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

        private static void ValoresPadrao(Transportadora result)
        {
            result.Pessoa.AdicionarTipoPessoa(TipoPessoa.Juridica);
            result.Pessoa.AdicionarPapel(TipoPapel.Estacionamento);
        }
    }
}
