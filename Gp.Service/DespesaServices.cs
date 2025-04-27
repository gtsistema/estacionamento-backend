using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Extensions;
using Gp.Domain.Input.Despesa;
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
        private readonly IDespesaLancamentoRepositories _despesaLancamentoRepo;
        private readonly IMapper _mapper;

        public DespesaServices(IErrorServices _errorServices, 
                               IDespesaRepositories repo, IMapper mapper, IDespesaLancamentoRepositories despesaLancamentoRepo) : base(_errorServices)
        {
            _repo = repo;
            _mapper = mapper;
            _despesaLancamentoRepo = despesaLancamentoRepo;
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

        public async Task<ActionResult> ImportarDadosExcelAsync(long id)
        {
            var result = await _repo.ExistAsync(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var despesa = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }

        public async Task<ActionResult> LancamentoAsync(DespesaLancamentoPostInput input)
        {
            input.AtribuirMesReferente();

            var despesa = await _despesaLancamentoRepo.GetIdByDescricaoAsync(input.DescricaoDespesa, input.MesReferente.Value);

            if (despesa == null)
                return await RetornNo(false,string.Format(@"Despesa: {0} não encontrado no mês: {1} !", input.DescricaoDespesa,  DataExtesions.ObterMesAtualString()));

            input.AtribuirDespesaId(despesa.Id);

            var despesaLancamento = _mapper.Map<DespesaLancamento>(input);


            await _despesaLancamentoRepo.InsertAsync(despesaLancamento);

            await _repo.AtualizarSaldoPagoAsync(despesa, despesaLancamento);

            return await RetornOk(_mapper.Map<DespesaDto>(despesa));
        }
    }
}
