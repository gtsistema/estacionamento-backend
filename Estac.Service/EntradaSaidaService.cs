using AutoMapper;
using Estac.Domain.Input.EntradaSaida;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Domain.Output.EntradaSaida;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class EntradaSaidaService : ServiceResult<EntradaSaidaOutput>, IEntradaSaidaService
    {
        private readonly IEntradaSaidaRepositories _repositories;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public EntradaSaidaService(
            IErrorServices errorServices,
            IEntradaSaidaRepositories repositories,
            IMapper mapper,
            ICurrentUser currentUser) : base(errorServices)
        {
            _repositories = repositories;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ActionResult> ObterPorId(int id)
        {
            try
            {
                var result = await _repositories.SelecionarPorIdCompleto(id);

                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> ObterPorPlaca(string placa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(placa))
                    return await RetornNo(false, "dados não encontrado");

                var result = await _repositories.SelecionarPorPlaca(placa);

                if (result is null)
                    return await RetornNo(false, "dados não encontrado");

                return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Buscar(EntradaSaidaFilterInput filter)
        {
            try
            {
                var result = await _repositories.Paginar(filter);
                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Gravar(EntradaSaidaPostInput input)
        {
            try
            {
                var validacaoHorarios = ValidarDatas(input.DataHoraEntrada, input.DataHoraSaida);
                if (validacaoHorarios is not null)
                    return await RetornNo(false, validacaoHorarios);

                var result = _mapper.Map<EntradaSaida>(input);
                result.DataHoraUltimaEntradaPatio = result.DataHoraEntrada;
                result.PermanenciaSuspensa = false;
                result.Finalizado = false;
                result.TempoPermanenciaMinutos = 0;
                result.TempoTotalSuspensaoMinutos = 0;
                result.UsuarioRegistroEntradaId = _currentUser.Id;
                result.UsuarioRegistroEntradaNome = _currentUser.Name;
                await _repositories.Gravar(result);

                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Alterar(EntradaSaidaPutInput input)
        {
            try
            {
                var validacaoHorarios = ValidarDatas(input.DataHoraEntrada, input.DataHoraSaida);
                if (validacaoHorarios is not null)
                    return await RetornNo(false, validacaoHorarios);

                var result = _mapper.Map<EntradaSaida>(input);
                await _repositories.Alterar(result);

                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> SuspenderPermanencia(int id, EntradaSaidaPermanenciaInput input)
        {
            try
            {
                var result = await _repositories.SelecionarParaControlePermanencia(id);
                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                if (result.Finalizado)
                    return await RetornNo(false, "Permanência já finalizada.");

                var dataEvento = input?.DataHoraEvento ?? DateTime.Now;

                if (input?.RetornarAoPatio == true)
                    return await RetornarAoPatio(result, dataEvento);

                return await SuspenderSaidaTemporaria(result, dataEvento);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> FinalizarPermanencia(int id, DateTime? dataHoraSaida)
        {
            try
            {
                var result = await _repositories.SelecionarParaControlePermanencia(id);
                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                if (result.Finalizado)
                    return await RetornNo(false, "Permanência já finalizada.");

                var dataFinalizacao = dataHoraSaida ?? DateTime.Now;
                if (dataFinalizacao < result.DataHoraEntrada)
                    return await RetornNo(false, "A data de finalização não pode ser menor que a data de entrada.");

                if (!result.PermanenciaSuspensa)
                {
                    if (!result.DataHoraUltimaEntradaPatio.HasValue)
                        result.DataHoraUltimaEntradaPatio = result.DataHoraEntrada;

                    AdicionarMinutosPermanencia(result, result.DataHoraUltimaEntradaPatio.Value, dataFinalizacao);
                }
                else
                {
                    var suspensaoAberta = result.Suspensoes
                        .Where(x => !x.DataHoraFimSuspensao.HasValue)
                        .OrderByDescending(x => x.DataHoraInicioSuspensao)
                        .FirstOrDefault();

                    if (suspensaoAberta is not null && dataFinalizacao >= suspensaoAberta.DataHoraInicioSuspensao)
                    {
                        var minutosSuspensao = (int)Math.Max(0, (dataFinalizacao - suspensaoAberta.DataHoraInicioSuspensao).TotalMinutes);
                        suspensaoAberta.DataHoraFimSuspensao = dataFinalizacao;
                        suspensaoAberta.TempoSuspensaoMinutos = minutosSuspensao;
                        result.TempoTotalSuspensaoMinutos += minutosSuspensao;
                    }
                }

                result.DataHoraSaida = dataFinalizacao;
                result.DataHoraFinalizacao = dataFinalizacao;
                result.UsuarioFinalizacaoId = _currentUser.Id;
                result.UsuarioFinalizacaoNome = _currentUser.Name;
                result.DataHoraUltimaEntradaPatio = null;
                result.PermanenciaSuspensa = false;
                result.Finalizado = true;

                await _repositories.Alterar(result);
                return await RetornOk(result);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                var existe = await _repositories.Existe(id);
                if (!existe)
                    return await RetornNo(false, "Registro não localizado na base de dados.");

                await _repositories.Excluir(id);
                return await RetornOk(true);
            }
            catch (Exception ex)
            {
                return await RetornNo(false, ex.Message);
            }
        }

        private static string ValidarDatas(DateTime dataHoraEntrada, DateTime? dataHoraSaida)
        {
            if (dataHoraEntrada == default)
                return "A data/hora de entrada é obrigatória.";

            if (dataHoraSaida.HasValue && dataHoraSaida.Value < dataHoraEntrada)
                return "A data/hora de saída não pode ser menor que a data/hora de entrada.";

            return null;
        }

        private async Task<ActionResult> SuspenderSaidaTemporaria(EntradaSaida result, DateTime dataEvento)
        {
            if (result.PermanenciaSuspensa)
                return await RetornNo(false, "Permanência já está suspensa.");

            if (!result.DataHoraUltimaEntradaPatio.HasValue)
                result.DataHoraUltimaEntradaPatio = result.DataHoraEntrada;

            if (dataEvento < result.DataHoraUltimaEntradaPatio.Value)
                return await RetornNo(false, "Data/hora da suspensão inválida.");

            AdicionarMinutosPermanencia(result, result.DataHoraUltimaEntradaPatio.Value, dataEvento);

            result.Suspensoes.Add(new EntradaSaidaSuspensao
            {
                DataHoraInicioSuspensao = dataEvento,
                UsuarioSuspensaoId = _currentUser.Id,
                UsuarioSuspensaoNome = _currentUser.Name
            });

            result.PermanenciaSuspensa = true;
            result.DataHoraUltimaEntradaPatio = null;

            await _repositories.Alterar(result);
            return await RetornOk(result);
        }

        private async Task<ActionResult> RetornarAoPatio(EntradaSaida result, DateTime dataEvento)
        {
            if (!result.PermanenciaSuspensa)
                return await RetornNo(false, "Permanência não está suspensa.");

            if (dataEvento < result.DataHoraEntrada)
                return await RetornNo(false, "Data/hora de retorno inválida.");

            var suspensaoAberta = result.Suspensoes
                .Where(x => !x.DataHoraFimSuspensao.HasValue)
                .OrderByDescending(x => x.DataHoraInicioSuspensao)
                .FirstOrDefault();

            if (suspensaoAberta is null)
                return await RetornNo(false, "Nenhuma suspensão em aberto foi encontrada.");

            if (dataEvento < suspensaoAberta.DataHoraInicioSuspensao)
                return await RetornNo(false, "Data/hora de retorno inválida.");

            var minutosSuspensao = (int)Math.Max(0, (dataEvento - suspensaoAberta.DataHoraInicioSuspensao).TotalMinutes);
            suspensaoAberta.DataHoraFimSuspensao = dataEvento;
            suspensaoAberta.TempoSuspensaoMinutos = minutosSuspensao;
            result.TempoTotalSuspensaoMinutos += minutosSuspensao;

            result.PermanenciaSuspensa = false;
            result.DataHoraUltimaEntradaPatio = dataEvento;

            await _repositories.Alterar(result);
            return await RetornOk(result);
        }

        private static void AdicionarMinutosPermanencia(EntradaSaida result, DateTime inicio, DateTime fim)
        {
            var minutos = (int)Math.Max(0, (fim - inicio).TotalMinutes);
            result.TempoPermanenciaMinutos += minutos;
        }
    }
}
