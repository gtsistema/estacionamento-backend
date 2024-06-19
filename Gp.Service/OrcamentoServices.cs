using AutoMapper;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Service
{
    public class OrcamentoServices : IOrcamentoServices
    {
        private readonly IOrcamentoRepositories _repo;
        private readonly IMapper _mapper;

        public OrcamentoServices(IOrcamentoRepositories repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServicesResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return new ServicesResult(resultado);
        }

        public async Task<ServicesResult> GetAllAsync(Orcamento item)
        {
            var resultado = await _repo.SelectAllAsync();

            return new ServicesResult(_mapper.Map<IEnumerable<Orcamento>>(resultado));
        }

        public async Task<ServicesResult> PostAsync(Orcamento item)
        {
            return new ServicesResult(await _repo.SelectAsync(item.Id));
        }

        public async Task<ServicesResult> PutAsync(Orcamento item)
        {
            var resultado = _mapper.Map<Orcamento>(item);

            return new ServicesResult(await _repo.UpdateAsync(resultado));
        }

        public async Task<ServicesResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
            {
                return new ServicesResult("Produto não localizado na base de dados!");
            }

            var resultado = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return new ServicesResult(true);
        }
    }
}
