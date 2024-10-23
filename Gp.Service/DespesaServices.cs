using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Dtos.Curso;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class DespesaServices : ServicesResult<DespesaDto>, IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IMapper _mapper;

        public DespesaServices(IErrorServices _errorServices, 
                               IDespesaRepositories repo, IMapper mapper) : base(_errorServices)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return await RetornOk(_mapper.Map<DespesaDto>(resultado));  
        }

        public async Task<ActionResult> GetAllAsync(DespesaFilterInput filter)
        {
            var resultado = await _repo.GetPageAsync(filter);

            var mapper = _mapper.Map<IEnumerable<DespesaDto>>(resultado.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> PostAsync(DespesaPostInput input)
        {
            var validations = DespesaPostInput.Validar(input);

            if (!validations.IsValid)
            {
                validations.Errors.ToList().ForEach(e => result.AdicionarErro(e.PropertyName, e.ErrorMessage));
                return result;
            }

            return new ActionResult();
        }

        public async Task<ActionResult> PutAsync(Despesa item)
        {
            var resultado = _mapper.Map<Despesa>(item);

            return new ActionResult(await _repo.UpdateAsync(resultado));
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
            {
                return new ActionResult("Produto não localizado na base de dados!");
            }

            var resultado = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return new ActionResult(true);
        }
    }
}
