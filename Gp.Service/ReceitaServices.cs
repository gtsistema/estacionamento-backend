using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class ReceitaServices : ServiceResult<Receita>, IReceitaServices
    {
        private readonly IReceitaRepositories _repo;
        private readonly IMapper _mapper;

        public ReceitaServices(IReceitaRepositories repo,
                               IMapper mapper,
                               IErrorServices _errorApplication
                               ) : base(_errorApplication)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ActionResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return await RetornOk(_mapper.Map<Despesa>(resultado));
        }

        public async Task<ActionResult> GetAllAsync(Receita item)
        {
            var resultado = await _repo.SelectAllAsync();

            return await RetornOk(_mapper.Map<IEnumerable<Receita>>(resultado));
        }

        public async Task<ActionResult> PostAsync(Receita item)
        {
            return await RetornOk(await _repo.InsertAsync(item));
        }

        public async Task<ActionResult> PutAsync(Receita input)
        {
            return await RetornOk(await _repo.UpdateAsync(input));
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
            {
                return await RetornOk("Produto não localizado na base de dados!");
            }

            var product = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }
    }
}
