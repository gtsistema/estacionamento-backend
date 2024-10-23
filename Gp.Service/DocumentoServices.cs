using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class DocumentoServices : ServiceResult<Despesa>, IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IMapper _mapper;

        public DocumentoServices(IDespesaRepositories repo,
                                 IMapper mapper,
                                 IErrorApplication errorApplication
                                 ) : base(errorApplication)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return await RetornOk(_mapper.Map<DespesaGetDto>(resultado));
        }

        public async Task<ActionResult> GetAllAsync(DespesaFilterInput filter)
        {
            var resultado = await _repo.GetPageAsync(filter);

            var mapper = _mapper.Map<IEnumerable<DespesaGetDto>>(resultado);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> PostAsync(DespesaPostInput input)
        {
            var validations = DespesaPostInput.Validar(input);

            //if (!validations.IsValid)
            //{
            //    var result = await RetornOk(null);
            //    validations.Errors.ToList().ForEach(e => result.AdicionarErro(e.PropertyName, e.ErrorMessage));
            //    return result;
            //}

            return await RetornOk(true);
        }

        public async Task<ActionResult> PutAsync(DespesaPutInput item)
        {
            var resultado = _mapper.Map<Despesa>(item);

            return await RetornOk(await _repo.UpdateAsync(resultado));
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
            {
                return await RetornOk("Produto não localizado na base de dados!");
            }

            var resultado = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }
    }
}
