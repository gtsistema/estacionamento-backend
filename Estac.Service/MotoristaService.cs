using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Estac.Domain.Input.Motorista;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Output.Motorista;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class MotoristaService : ServiceResult<MotoristaOutput>, IMotoristaService
    {
        private readonly IMotoristaRepositories _repositories;
        private readonly IMapper _mapper;

        public MotoristaService(IErrorServices _errorServices,
                               IMotoristaRepositories repositories, IMapper mapper) : base(_errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.Selecionar(id);

            return await RetornOk(_mapper.Map<MotoristaOutput>(result));
        }

        public async Task<ActionResult> Buscar(MotoristaFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            var mapper = _mapper.Map<IEnumerable<MotoristaOutput>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> Gravar(MotoristaPostInput input)
        {
            try
            {
                //var validations = MotoristaPostInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Motorista>(input);

                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex) 
            {
                return await RetornNo(false, ex.Message);
            }
          
        }

        public async Task<ActionResult> Alterar(MotoristaPutInput input)
        {
            try
            {
                //var validations = MotoristaPutInput.Validar(input);

                //if (!validations.IsValid)
                //    return await RetornNo(false, validations.Errors);

                var result = _mapper.Map<Motorista>(input);
                await _repositories.Alterar(result);

                result.Pessoa.AdicionarPapel(TipoPapel.Motorista);

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
