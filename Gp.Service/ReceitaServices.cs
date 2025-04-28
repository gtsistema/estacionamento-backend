using AutoMapper;
using Gp.Domain.Dtos;
using Gp.Domain.Extensions;
using Gp.Domain.Input.Receita;
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
        private readonly IReceitaLancamentoRepositories _receitaLancamentoRepo;

        public ReceitaServices(IReceitaRepositories repo,
                               IMapper mapper,
                               IErrorServices _errorApplication,
                               IReceitaLancamentoRepositories receitaLancamentoRepo
                               ) : base(_errorApplication)
        {
            _repo = repo;
            _mapper = mapper;
            _receitaLancamentoRepo = receitaLancamentoRepo;
        }

        public async Task<ActionResult> GetAsync(long id)
        {
            var result = await _repo.SelectAsync(id);

            return await RetornOk(_mapper.Map<ReceitaDto>(result));
        }

        public async Task<ActionResult> GetAllAsync(ReceitaFilterInput filter)
        {
            var result = await _repo.GetPageAsync(filter);

            var mapper = _mapper.Map<IEnumerable<ReceitaDto>>(result.Results);

            return await RetornOk(mapper);
        }

        public async Task<ActionResult> PostAsync(ReceitaPostInput input)
        {
            var validations = ReceitaPostInput.Validar(input);

            if (!validations.IsValid)


                return await RetornNo(false, validations.Errors);

            var result = _mapper.Map<Receita>(input);

            await _repo.InsertAsync(result);

            return await RetornOk(result);
        }

        public async Task<ActionResult> PutAsync(ReceitaPutInput input)
        {
            var validations = ReceitaPutInput.Validar(input);

            if (!validations.IsValid)
                return await RetornNo(false, validations.Errors);

            var result = _mapper.Map<Receita>(input);

            return await RetornOk(await _repo.UpdateAsync(result));
        }

        public async Task<ActionResult> DeleteAsync(long id)
        {
            var result = await _repo.ExistAsync(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var receita = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }

        public async Task<ActionResult> ImportarDadosExcelAsync(long id)
        {
            var result = await _repo.ExistAsync(id);

            if (!result)
                return await RetornNo(false, "Produto não localizado na base de dados!");

            var receita = await _repo.SelectAsync(id);

            await _repo.DeleteAsync(id);

            return await RetornOk(true);
        }

        public async Task<ActionResult> LancamentoAsync(ReceitaLancamentoPostInput input)
        {
            input.AtribuirMesReferente();

            var receita = await _receitaLancamentoRepo.GetIdByDescricaoAsync(input.DescricaoReceita, input.MesReferente.Value);

            if (receita == null)
                return await RetornNo(false, string.Format(@"Receita: {0} não encontrado no mês: {1} !", input.DescricaoReceita, DataExtesions.ObterMesAtualString()));

            input.AtribuirReceitaId(receita.Id);

            var receitaLancamento = _mapper.Map<ReceitaLancamento>(input);


            await _receitaLancamentoRepo.InsertAsync(receitaLancamento);

            await _repo.AtualizarSaldoPagoAsync(receita, receitaLancamento);

            return await RetornOk(_mapper.Map<ReceitaDto>(receita));
        }
    }
}
