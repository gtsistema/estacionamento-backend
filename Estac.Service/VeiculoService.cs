using AutoMapper;
using Estac.Domain.Input.Veiculo;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Output;
using Estac.Domain.Output.Veiculo;
using Estac.Infra.Repositories;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estac.Service
{
    public class VeiculoService : ServiceResult<VeiculoOutput>, IVeiculoService
    {
        private readonly IVeiculoRepositories _repositories;
        private readonly ITransportadoraRepositories _transportadoraRepositories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VeiculoService(IErrorServices _errorServices,
                               IVeiculoRepositories repositories,
                               ITransportadoraRepositories transportadoraRepositories,
                               IMapper mapper,
                               IUnitOfWork unitOfWork) : base(_errorServices)
        {
            _repositories = repositories;
            _transportadoraRepositories = transportadoraRepositories;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            var result = await _repositories.SelecionarPorIdCompleto(id);

            return await RetornOk(_mapper.Map<VeiculoOutput>(result));
        }

        public async Task<ActionResult> Buscar(VeiculoFilterInput filter)
        {
            var result = await _repositories.Paginar(filter);

            return await RetornOk(result);
        }

        public async Task<ActionResult> Gravar(VeiculoPostInput input)
        {
            try
            {
                var transportadoraInvalida = await Validar(input.TransportadoraId);

                if (transportadoraInvalida != null)
                    return transportadoraInvalida;

                var veiculo = _mapper.Map<Veiculo>(input);
                var salvo = await _repositories.GravarCompleto(veiculo);
                var completo = await _repositories.SelecionarPorIdCompleto(salvo.Id);
                return await RetornOk(_mapper.Map<VeiculoOutput>(completo));
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(VeiculoPutInput input)
        {
            var transportadoraInvalida = await Validar(input.TransportadoraId);
            if (transportadoraInvalida != null)
                return transportadoraInvalida;

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var veiculo = _mapper.Map<Veiculo>(input);
                var atualizado = await _repositories.AlterarCompleto(veiculo);

                if (atualizado == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return await RetornNo(false, "Veículo não localizado na base de dados.");
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                var completo = await _repositories.SelecionarPorIdCompleto(atualizado.Id);
                return await RetornOk(_mapper.Map<VeiculoOutput>(completo));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var transportadora = await _repositories.ExcluirCompleto(id);

                if (!transportadora)
                    return await RetornNo(false, "Veículo não localizado na base de dados.");

                return await RetornOk(true);
            }
            catch (DbUpdateException)
            {
                return await RetornNo(false,
                    "Não é possível excluir o veículo: existem registros vinculados (ex.: movimentações de entrada/saída ou outros vínculos).");
            }
        }

        private async Task<ActionResult> Validar(int? transportadoraId)
        {
            var existe = await _transportadoraRepositories.SelecionarIdSimplificado(transportadoraId ?? transportadoraId.Value);

            if (existe is null)
                return await RetornNo(false, "Transportadora não localizada na base de dados.");

            return null;
        }

        public async Task<ActionResult> ObterVinculosPorPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return await RetornNo(false, "Placa informada é inválida.");

            var result = await _repositories.ObterVinculosPorPlaca(placa);

            if (result == null)
                return await RetornNo(false, "Veículo não localizado na base de dados.", 404);

            return await RetornOk(result);
        }
    }
}
