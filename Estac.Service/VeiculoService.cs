using AutoMapper;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Output;
using Estac.Domain.Output.Veiculo;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class VeiculoService : ServiceResult<VeiculoOutput>, IVeiculoService
    {
        private readonly IVeiculoRepositories _repositories;
        private readonly IMapper _mapper;

        public VeiculoService(IErrorServices _errorServices,
                               IVeiculoRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.Selecionar(id);

            return await RetornOk(_mapper.Map<VeiculoOutput>(result));
        }

        public async Task<ActionResult> Buscar(VeiculoFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            var mapper = _mapper.Map<IEnumerable<VeiculoOutput>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> Gravar(VeiculoPostInput input)
        {
            try
            {
                //var validations = VeiculoPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Veiculo>(input);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(VeiculoPutInput input)
        {
            //var validations = VeiculoPutInput.Validar(input);

            //if (!validations.IsValid)
            //    return await RetornNo(false, validations.Errors);

            var result = _mapper.Map<Veiculo>(input);

            return await RetornOk(await _repositories.Alterar(result));
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
