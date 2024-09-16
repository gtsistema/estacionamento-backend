using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Input;
using Gp.Domain.Interface.Repositories;
using Gp.Domain.Interface.Services;
using Gp.Domain.Models;
using Gp.Domain.Output;
using Gp.Infra.Migrations;
using Gp.Service.Extensions;
using Gp.Service.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Gp.Service
{
    public class DespesaServices : ServiceResult<Despesa>, IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IMapper _mapper;

        public DespesaServices(IDespesaRepositories repo,
                               IMapper mapper,
                               IErrorApplication _errorApplication
                               ) : base(_errorApplication)
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

            return await RetornOk(_mapper.Map<IEnumerable<DespesaGetDto>>(resultado));
        }

        public async Task<ActionResult> PostAsync(DespesaPostInput input)
        {
            var validacao = DespesaPostInput.Validar(input);

            if (!validacao.IsValid)
                return await RetornNo(false, validacao.Errors);

            var despesa = _mapper.Map<Despesa>(input);
            var inserir = await _repo.InsertAsync(despesa);

            return await RetornOk(_mapper.Map<DespesaPostOutput>(input));
        }

        public async Task<ActionResult> PutAsync(DespesaPutInput input)
        {
            var validacao = DespesaPutInput.Validar(input);

            if (!validacao.IsValid)
                return await RetornNo(false, validacao.Errors);

            var resultado = _mapper.Map<Despesa>(input);

            return await RetornOk(await _repo.UpdateAsync(_mapper.Map<Despesa>(resultado)));
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _repo.ExistAsync(id))
                return await RetornNo(Resources.Resources.MSG_Nao_localizado, Resources.Resources.Produto);

            var resultado = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }
    }
}
