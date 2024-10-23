using AutoMapper;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class OrcamentoServices : ServiceResult<Orcamento>, IOrcamentoServices
    {
        private readonly IOrcamentoRepositories _repo;
        private readonly IMapper _mapper;

        public OrcamentoServices(IOrcamentoRepositories repo,
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

            return await RetornOk(resultado);
        }

        public async Task<ActionResult> GetAllAsync(Orcamento item)
        {
            var resultado = await _repo.SelectAllAsync();

            return await RetornOk(_mapper.Map<IEnumerable<Orcamento>>(resultado));
        }

        public async Task<ActionResult> PostAsync(Orcamento item)
        {
            return await RetornOk(await _repo.SelectAsync(item.Id));
        }

        public async Task<ActionResult> PutAsync(Orcamento item)
        {
            var resultado = _mapper.Map<Orcamento>(item);

            return await RetornOk(await _repo.UpdateAsync(resultado));
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var resultado = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }
    }
}
