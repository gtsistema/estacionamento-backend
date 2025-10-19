using AutoMapper;
using Estac.Domain.Dtos;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Despesa;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class DespesaServices : ServiceResult<DespesaDto>, IDespesaServices
    {
        private readonly IDespesaRepositories _repo;
        private readonly IDespesaLancamentoRepositories _despesaLancamentoRepo;
        private readonly IDespesaPagamentoRepositories _despesaPagamentoRepo;
        private readonly IMapper _mapper;

        public DespesaServices(IErrorServices _errorServices, 
                               IDespesaRepositories repo, IMapper mapper, IDespesaLancamentoRepositories despesaLancamentoRepo, IDespesaPagamentoRepositories despesaPagamentoRepo) : base(_errorServices)
        {
            _repo = repo;
            _mapper = mapper;
            _despesaLancamentoRepo = despesaLancamentoRepo;
            _despesaPagamentoRepo = despesaPagamentoRepo;
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
            Despesa despesa = new Despesa();

            if (input.CompraParcelado)
            {
                input.AtribuirMesReferente();

                string descricaoOriginal = input.Descricao;

                for (int i = 1; i <= input.QuantidadeParcelado; i++)
                {
                    despesa = await _despesaLancamentoRepo.GetIdByDescricaoAsync(input.DescricaoDespesa, input.MesReferente.Value);

                    if (despesa == null)
                    {
                        despesa = new Despesa()
                        {
                            MesReferente = input.MesReferente.Value,
                            Descricao = input.DescricaoDespesa,
                            OrcamentoId = input.OrcamentoId.HasValue ? input.OrcamentoId.Value : 1,
                            ValorPago = 0,
                            ValorTotal = input.Valor,
                            PorcentagemPaga = 0,
                            SaldoRestante = input.Valor,
                            TipoDespesa = EnumExtesions.ObterEnumPorDescricao<TipoDespesa>(input.DescricaoDespesa)
                        };

                        await _repo.InsertAsync(despesa);

                    }

                    if (input.Cartao.HasValue)
                    {
                        input.Descricao = $"{descricaoOriginal} - Parcela {i}/{input.QuantidadeParcelado}";
                        input.Pagamento = "Parcelado";
                    }

                    input.DespesaId = despesa.Id;

                    var despesaLancamento = _mapper.Map<DespesaLancamento>(input);
                    await _despesaLancamentoRepo.InsertAsync(despesaLancamento);
                    await _repo.AtualizarSaldoDevedorAsync(despesa, despesaLancamento);

                    input.MesReferente = (MesDoAno)((int)input.MesReferente + 1);
                }
            }

            else
            {
                despesa = await _despesaLancamentoRepo.GetIdByDescricaoAsync(input.DescricaoDespesa, input.MesReferente.Value);

                if (despesa == null)
                {
                    despesa = new Despesa()
                    {
                        MesReferente = input.MesReferente.Value,
                        Descricao = input.DescricaoDespesa,
                        OrcamentoId = input.OrcamentoId.HasValue ? input.OrcamentoId.Value : 1,
                        ValorPago = 0,
                        ValorTotal = input.Valor,
                        PorcentagemPaga = 0,
                        SaldoRestante = input.Valor,
                        TipoDespesa = EnumExtesions.ObterEnumPorDescricao<TipoDespesa>(input.DescricaoDespesa)
                    };

                    await _repo.InsertAsync(despesa);
                }

                input.Pagamento = "Avista";
                input.DespesaId = despesa.Id;
                var despesaLancamento = _mapper.Map<DespesaLancamento>(input);
                await _despesaLancamentoRepo.InsertAsync(despesaLancamento);
                await _repo.AtualizarSaldoDevedorAsync(despesa, despesaLancamento);

            }

            return await RetornOk(true);
        }

        public async Task<ActionResult> PagamentoAsync(DespesaPagamentoPostInput input)
        {
            input.AtribuirMesReferente();

            var despesa = await _despesaPagamentoRepo.GetIdByDescricaoAsync(input.DescricaoDespesa, input.MesReferente.Value);

            if (despesa == null)
                return await RetornNo(false, string.Format(@"Despesa: {0} não encontrado no mês: {1} !", input.DescricaoDespesa, DataExtesions.ObterMesAtualString()));

            input.AtribuirDespesaId(despesa.Id);

            var despesaPagamento = _mapper.Map<DespesaPagamento>(input);

            await _despesaPagamentoRepo.InsertAsync(despesaPagamento);

            await _repo.AtualizarSaldoPagoAsync(despesa, despesaPagamento);

            return await RetornOk(_mapper.Map<DespesaDto>(despesa));
        }

        public async Task<ActionResult> AtualizarSaldoDevadorDoMesAsync(AtualizaSaldoDoMesPostInput input)
        {
            await _repo.AtualizarSaldoDevadorDoMesAsync(input);

            return await RetornOk(true);
        }

    }
}
