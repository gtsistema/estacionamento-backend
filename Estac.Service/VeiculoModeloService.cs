using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.VeiculoModelo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.VeiculoModelo;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class VeiculoModeloService : ServiceResult<VeiculoModeloOutput>, IVeiculoModeloService
    {
        private readonly IVeiculoModeloRepositories _repositories;
        private readonly IMapper _mapper;

        public VeiculoModeloService(IErrorServices _errorServices,
                               IVeiculoModeloRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.Selecionar(id);

            return await RetornOk(_mapper.Map<VeiculoModeloOutput>(result));
        }

        public async Task<ActionResult> Buscar(VeiculoModeloFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            var mapper = _mapper.Map<IEnumerable<VeiculoModeloOutput>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> Gravar(VeiculoModeloPostInput input)
        {
            try
            {
                //var validations = VeiculoModeloPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<VeiculoModelo>(input);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(VeiculoModeloPutInput input)
        {
            try
            {
                //var validations = VeiculoModeloPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<VeiculoModelo>(input);
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

            var despesa = await _repositories.Selecionar(id);

            await _repositories.Excluir(id);

            return await RetornOk(true);
        }

       
    }
}
