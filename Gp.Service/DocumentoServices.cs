using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;

namespace Gp.Service
{
    public class DocumentoServices : IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IMapper _mapper;

        public DocumentoServices(IDespesaRepositories repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ServicesResult> GetAsync(int id)
        {
            var resultado = await _repo.SelectAsync(id);

            return new ServicesResult(_mapper.Map<DespesaGetDto>(resultado));
        }

        public async Task<ServicesResult> GetAllAsync(DespesaFilterInput filter)
        {
            var resultado = await _repo.GetPageAsync(filter);

            var mapper = _mapper.Map<IEnumerable<DespesaGetDto>>(resultado.Dados);

            return new ServicesResult(mapper);
        }

        public async Task<ServicesResult> PostAsync(DespesaPostInput input)
        {
            var validations = DespesaPostInput.Validar(input);

            if (!validations.IsValid)
            {
                var result = new ServicesResult(null);
                validations.Errors.ToList().ForEach(e => result.AdicionarErro(e.PropertyName, e.ErrorMessage));
                return result;
            }

            return new ServicesResult();
        }

        public async Task<ServicesResult> PutAsync(Despesa item)
        {
            var resultado = _mapper.Map<Despesa>(item);

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
