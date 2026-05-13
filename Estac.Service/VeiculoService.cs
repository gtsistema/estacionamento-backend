using AutoMapper;
using Estac.Domain.Input.Motorista;
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
        private readonly IVeiculoModeloRepositories _veiculoModeloRepositories;
        private readonly IMotoristaRepositories _motoristaRepositories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VeiculoService(IErrorServices _errorServices,
                               IVeiculoRepositories repositories,
                               ITransportadoraRepositories transportadoraRepositories,
                               IVeiculoModeloRepositories veiculoModeloRepositories,
                               IMotoristaRepositories motoristaRepositories,
                               IMapper mapper,
                               IUnitOfWork unitOfWork) : base(_errorServices)
        {
            _repositories = repositories;
            _transportadoraRepositories = transportadoraRepositories;
            _veiculoModeloRepositories = veiculoModeloRepositories;
            _motoristaRepositories = motoristaRepositories;
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
                var referenciasInvalidas = await ValidarReferenciasAsync(input);

                if (referenciasInvalidas != null)
                    return referenciasInvalidas;

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
            var referenciasInvalidas = await ValidarReferenciasAsync(input);
            if (referenciasInvalidas != null)
                return referenciasInvalidas;

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
            if (!await _repositories.Existe(id))
                return await RetornNo(false, "Veículo não localizado na base de dados.", statusCode: 404);

            if (await _repositories.PossuiMotoristaVinculadoAsync(id))
                return await RetornNo(false, "Não é possível excluir: existem motoristas vinculados a este veículo.");

            try
            {
                var excluido = await _repositories.ExcluirCompleto(id);

                if (!excluido)
                    return await RetornNo(false, "Veículo não localizado na base de dados.", statusCode: 404);

                return await RetornOk(true);
            }
            catch (DbUpdateException)
            {
                return await RetornNo(false,
                    "Não é possível excluir o veículo: existem registros vinculados (ex.: movimentações de entrada/saída ou outros vínculos).");
            }
        }

        private async Task<ActionResult> ValidarReferenciasAsync(VeiculoPostInput input)
        {
            if (input.TransportadoraId.HasValue)
            {
                var transportadora = await _transportadoraRepositories.SelecionarIdSimplificado(input.TransportadoraId.Value);
                if (transportadora is null)
                    return await RetornNo(false, "Transportadora não localizada na base de dados.");
            }

            var modeloId = input.Modelo?.Id > 0 ? input.Modelo.Id : 0;
            if (modeloId > 0)
            {
                if (!await _veiculoModeloRepositories.Existe(modeloId))
                    return await RetornNo(false, "Modelo de veículo não localizado na base de dados.");

                var marcaInformada = input.Marca?.Id > 0
                    ? input.Marca.Id
                    : (input.Modelo.Marca?.Id > 0 ? input.Modelo.Marca.Id : 0);

                if (marcaInformada > 0)
                {
                    var modelo = await _veiculoModeloRepositories.Selecionar(modeloId);
                    if (modelo != null && modelo.VeiculoMarcaId != marcaInformada)
                        return await RetornNo(false, "Marca informada não corresponde ao modelo de veículo.");
                }
            }

            var motoristaIds = (input.Motoristas ?? new List<MotoristaVinculoInput>())
                .Where(m => m != null && m.Id > 0)
                .Select(m => m.Id)
                .Distinct();

            foreach (var motoristaId in motoristaIds)
            {
                if (!await _motoristaRepositories.Existe(motoristaId))
                    return await RetornNo(false, "Motorista não localizado na base de dados.");
            }

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
