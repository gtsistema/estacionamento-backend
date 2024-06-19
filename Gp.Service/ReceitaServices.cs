using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Service
{
    public class ReceitaServices : IReceitaServices
    {
        private readonly IReceitaRepositories _repo;
        private readonly IMapper _mapper;

        public ReceitaServices(IReceitaRepositories repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServicesResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return new ServicesResult(_mapper.Map<DespesaGetDto>(resultado));
        }

        public async Task<ServicesResult> GetAllAsync(Receita item)
        {
            var resultado = await _repo.SelectAllAsync();

            return new ServicesResult(_mapper.Map<IEnumerable<Receita>>(resultado));
        }

        public async Task<ServicesResult> PostAsync(Receita item)
        {
            return new ServicesResult(await _repo.InsertAsync(item));
        }

        public async Task<ServicesResult> PutAsync(Receita input)
        {
            return new ServicesResult(await _repo.UpdateAsync(input));
        }

        public async Task<ServicesResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
            {
                return new ServicesResult("Produto não localizado na base de dados!");
            }

            var product = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return new ServicesResult(true);
        }
    }
}
