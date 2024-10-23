using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Dtos.Curso;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class DespesaServices : ServiceResult<DespesaDto>, IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IMapper _mapper;

        public DespesaServices(IErrorServices _errorServices, 
                               IDespesaRepositories repo, IMapper mapper) : base(_errorServices)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetAsync(long id)
        {
            var result = await _repo.SelectAsync(id);

            return await RetornOk(_mapper.Map<DespesaDto>(result));  
        }

        public async Task<ActionResult> GetAllAsync(DespesaFilterInput filter)
        {
            var result = await _repo.GetPageAsync(filter);

            var mapper = _mapper.Map<IEnumerable<DespesaDto>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> PostAsync(DespesaPostInput input)
        {
            var validations = DespesaPostInput.Validar(input);

            if (!validations.IsValid)
                return await RetornNo(false, validations.Errors);

            var result = _mapper.Map<Despesa>(input);

            await _repo.InsertAsync(result);

            return await RetornOk(result);
        }

        public async Task<ActionResult> PutAsync(DespesaPutInput input)
        {
            var validations = DespesaPutInput.Validar(input);

            if (!validations.IsValid)
                return await RetornNo(false, validations.Errors);

            var result = _mapper.Map<Despesa>(input);

            return await RetornOk(await _repo.UpdateAsync(result));
        }

        public async Task<ActionResult> DeleteAsync(long id)
        {
            var result = await _repo.ExistAsync(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var despesa = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }
    }
}
