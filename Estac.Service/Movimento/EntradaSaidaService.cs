using AutoMapper;
using Estac.Domain.Extensions;
using Estac.Domain.Input.Movimento.Entrada;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Output;
using Estac.Domain.Shared;
using Estac.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Estac.Domain.Input.Movimento.EntradaSaida;
using Estac.Domain.Output.Movimento.EntradaSaida;
using Estac.Domain.Integration.Workers;
using Estac.Domain.Interface.Integration;

namespace Estac.Service.Movimento
{
    public class EntradaSaidaService : ServiceResult<EntradaSaidaOutput>, IEntradaSaidaService
    {
        private readonly IEntradaSaidaRepositories _repositories;
        private readonly IMotoristaRepositories _motoristaRepositories;
        private readonly ITransportadoraRepositories _transportadoraRepositories;
        private readonly IVeiculoRepositories _veiculoRepositories;
        private readonly IVeiculoMotoristaRepositories _veiculoMotoristaRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IEstacionamentoWorkersClient _estacionamentoWorkers;

        public EntradaSaidaService(
            IErrorServices errorServices,
            IEntradaSaidaRepositories repositories,
            IMotoristaRepositories motoristaRepositories,
            ITransportadoraRepositories transportadoraRepositories,
            IVeiculoRepositories veiculoRepositories,
            IVeiculoMotoristaRepositories veiculoMotoristaRepositories,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUser currentUser,
            IEstacionamentoWorkersClient estacionamentoWorkers) : base(errorServices)
        {
            _repositories = repositories;
            _motoristaRepositories = motoristaRepositories;
            _transportadoraRepositories = transportadoraRepositories;
            _veiculoRepositories = veiculoRepositories;
            _veiculoMotoristaRepositories = veiculoMotoristaRepositories;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _estacionamentoWorkers = estacionamentoWorkers;
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
                    return await RetornNo(false, "Placa não informada.");

                var result = await _veiculoRepositories.ObterVinculosPorPlaca(placa);

                if (result is null)
                    return await RetornNo(false, "Veículo não localizado na base de dados.", 404);

                var entradaEmAberto = await _repositories.SelecionarEmAbertoPorPlaca(placa);
                if (entradaEmAberto != null)
                {
                    result.ExisteEntradaEmAberto = true;
                    result.Id = entradaEmAberto.Id;
                    result.DataHoraEntrada = entradaEmAberto.DataHoraEntrada;
                    result.Observacao = entradaEmAberto.Observacao;
                    result.Status = entradaEmAberto.Status;
                }
                else
                {
                    result.ExisteEntradaEmAberto = false;
                    result.Id = null;
                    result.DataHoraEntrada = null;
                    result.Observacao = null;
                    result.Status = null;
                }

                return await RetornOk(result);
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

        public async Task<ActionResult> Saida(EntradaSaidaPlacaInput input)
        {
            if (input is null || string.IsNullOrWhiteSpace(input.Placa))
                return await RetornNo(false, "Placa é obrigatória.");

            var entradaEmAberto = await _repositories.SelecionarEmAbertoPorPlaca(input.Placa);

            if (entradaEmAberto is null)
                return await RetornNo(false, "Nenhuma entrada em aberto encontrada para a placa informada.");

            return await RegistrarSaida(entradaEmAberto.Id, DateTime.Now);
        }

        public async Task<ActionResult> Gravar(EntradaPostInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados de entrada são obrigatórios.");

            if (input.Motorista is null || input.Veiculo is null)
                return await RetornNo(false, "Motorista e veículo são obrigatórios.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var result = _mapper.Map<EntradaSaida>(input);

                await TratamentoEntradaSaida(input, result);

                await _repositories.Gravar(result);
                await _veiculoMotoristaRepositories.VincularAsync(result.VeiculoId, result.MotoristaId);
                await _unitOfWork.CommitAsync();

                await NotificarWorkersMovimentacaoTempoRealAsync(result.Id);

                return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
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

        /// <summary>
        /// Finaliza a suspensão em aberto e retorna o caminhão ao pátio (status Entrada).
        /// </summary>
        public async Task<ActionResult> FinalizarPermanencia(int id, DateTime? dataHoraEvento)
        {
            try
            {
                var result = await _repositories.SelecionarParaControlePermanencia(id);
                if (result is null)
                    return await RetornNo(false, "Registro não encontrado.");

                if (result.Finalizado)
                    return await RetornNo(false, "Permanência já finalizada.");

                return await RetornarAoPatio(result, dataHoraEvento ?? DateTime.Now);
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

        private async Task<int> ResolverMotoristaId(EntradaMotoristaInput motoristaInput)
        {
            if (motoristaInput.Id.HasValue && motoristaInput.Id.Value > 0)
                return motoristaInput.Id.Value;

            var cpf = motoristaInput.Cpf.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF do motorista é obrigatório para novo cadastro.");

            var nome = string.IsNullOrWhiteSpace(motoristaInput.Nome) ? "Motorista" : motoristaInput.Nome.Trim();

            var motoristaExistente = await _motoristaRepositories.SelectAllAsync();
            var existente = await motoristaExistente
                .AsNoTracking()
                .Where(x => x.Pessoa != null && x.Pessoa.Documento == cpf)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (existente > 0)
                return existente;

            var novo = new Motorista
            {
                Descricao = nome,
                CNH = cpf,
                Pessoa = new Pessoa
                {
                    Documento = cpf,
                    NomeRazaoSocial = nome,
                    Descricao = nome,
                    Ativo = true,
                    TipoPessoa = TipoPessoa.Fisica,
                    Papeis = new List<PessoaPapel> { new() { TipoPapel = TipoPapel.Motorista } }
                }
            };

            await _motoristaRepositories.Gravar(novo);
            return novo.Id;
        }

        private async Task<int?> ResolverTransportadoraId(EntradaTransportadoraInput transportadoraInput)
        {
            if (transportadoraInput is null || string.IsNullOrEmpty(transportadoraInput.Cnpj))
                return null;

            if (transportadoraInput.Id.HasValue && transportadoraInput.Id.Value > 0)
                return transportadoraInput.Id.Value;

            var cnpj = transportadoraInput.Cnpj.SomenteDigitos();
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("CNPJ da transportadora é obrigatório para novo cadastro.");

            var razaoSocial = string.IsNullOrWhiteSpace(transportadoraInput.RazaoSocial)
                ? "Transportadora"
                : transportadoraInput.RazaoSocial.Trim();

            var transportadoras = await _transportadoraRepositories.SelectAllAsync();
            var existente = await transportadoras
                .AsNoTracking()
                .Where(x => x.Pessoa != null && x.Pessoa.Documento == cnpj)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (existente > 0)
                return existente;

            var nova = new Transportadora
            {
                Descricao = razaoSocial,
                ResponsavelLegal = transportadoraInput.ResponsavelLegal,
                ResponsavelCpf = transportadoraInput.ResponsavelCpf.SomenteDigitos(),
                ResponsavelEmail = transportadoraInput.ResponsavelEmail,
                ResponsavelTelefone = transportadoraInput.ResponsavelTelefone.SomenteDigitos(),
                Pessoa = new Pessoa
                {
                    Documento = cnpj,
                    NomeRazaoSocial = razaoSocial,
                    Descricao = razaoSocial,
                    Ativo = true,
                    TipoPessoa = TipoPessoa.Juridica,
                    Papeis = new List<PessoaPapel> { new() { TipoPapel = TipoPapel.Tranportadora } }
                }
            };

            await _transportadoraRepositories.Gravar(nova);
            return nova.Id;
        }

        private async Task<int> ResolverVeiculoId(EntradaVeiculoInput veiculoInput, int? transportadoraId)
        {
            if (veiculoInput.Id.HasValue && veiculoInput.Id.Value > 0)
            {
                if (transportadoraId.HasValue)
                    await VincularTransportadoraAoVeiculo(veiculoInput.Id.Value, transportadoraId.Value);
                return veiculoInput.Id.Value;
            }

            var placa = VeiculoPlacaHelper.Normalizar(veiculoInput.Placa);
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa do veículo é obrigatória para novo cadastro.");

            var veiculos = await _veiculoRepositories.SelectAllAsync();
            var existente = await veiculos
                .AsNoTracking()
                .Where(x => x.Placa == placa)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (existente > 0)
            {
                if (transportadoraId.HasValue)
                    await VincularTransportadoraAoVeiculo(existente, transportadoraId.Value);
                return existente;
            }

            var novo = new Veiculo
            {
                Placa = placa,
                Descricao = placa,
                TipoCarga = veiculoInput.TipoCarga,
                Ativo = true,
                TransportadoraId = transportadoraId
            };

            await _veiculoRepositories.Gravar(novo);
            return novo.Id;
        }

        private async Task VincularTransportadoraAoVeiculo(int veiculoId, int transportadoraId)
        {
            var veiculo = await _veiculoRepositories.Selecionar(veiculoId);
            if (veiculo == null)
                throw new ArgumentException("Veículo informado não foi encontrado.");

            if (veiculo.TransportadoraId == transportadoraId)
                return;

            veiculo.TransportadoraId = transportadoraId;
            await _veiculoRepositories.Alterar(veiculo);
        }

        private async Task<int?> ObterTransportadoraDoVeiculo(int veiculoId)
        {
            var veiculo = await _veiculoRepositories.Selecionar(veiculoId);
            return veiculo?.TransportadoraId;
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
            result.Status = EntradaSaidaStatus.Suspenso;

            await _repositories.Alterar(result);
            await NotificarWorkersMovimentacaoTempoRealAsync(result.Id);

            return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
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
            result.Status = EntradaSaidaStatus.Entrada;

            await _repositories.Alterar(result);
            await NotificarWorkersMovimentacaoTempoRealAsync(result.Id);

            return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
        }

        private async Task<ActionResult> RegistrarSaida(int id, DateTime? dataHoraSaida)
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
            result.Status = EntradaSaidaStatus.Saida;

            await _repositories.Alterar(result);
            await NotificarWorkersMovimentacaoTempoRealAsync(result.Id);

            return await RetornOk(_mapper.Map<EntradaSaidaOutput>(result));
        }

        private static void AdicionarMinutosPermanencia(EntradaSaida result, DateTime inicio, DateTime fim)
        {
            var minutos = (int)Math.Max(0, (fim - inicio).TotalMinutes);
            result.TempoPermanenciaMinutos += minutos;
        }

        private async Task NotificarWorkersMovimentacaoTempoRealAsync(int entradaSaidaId)
        {
            var completo = await _repositories.SelecionarPorIdCompleto(entradaSaidaId);
            if (completo is null)
                return;

            var response = await _estacionamentoWorkers.RegistrarMovimentacaoTempoRealAsync(
                MontarMovimentacaoTempoReal(completo));

            if (!string.IsNullOrWhiteSpace(response?.Hub))
                await _repositories.AtualizarObservacaoAsync(entradaSaidaId, response.Hub);
        }

        private static MovimentacaoTempoRealRequest MontarMovimentacaoTempoReal(EntradaSaida result)
        {
            var placa = VeiculoPlacaHelper.Normalizar(result.Veiculo?.Placa ?? string.Empty);
            var motoristaNome =
                result.Motorista?.Pessoa?.NomeRazaoSocial?.Trim()
                ?? result.Motorista?.Pessoa?.Descricao?.Trim()
                ?? result.Motorista?.Descricao?.Trim()
                ?? "Motorista";

            var tipoCarga = result.Veiculo?.TipoCarga.HasValue == true
                ? result.Veiculo.TipoCarga.Value.GetDescription()
                : string.Empty;

            var transportadoraNome =
                result.Transportadora?.Pessoa?.NomeRazaoSocial?.Trim()
                ?? result.Transportadora?.Pessoa?.Descricao?.Trim()
                ?? result.Transportadora?.Descricao?.Trim()
                ?? string.Empty;

            return new MovimentacaoTempoRealRequest
            {
                Id = Guid.NewGuid(),
                Placa = placa,
                Motorista = motoristaNome,
                CPF = result.Motorista?.Pessoa?.Documento.SomenteDigitos() ?? string.Empty,
                Transportadora = transportadoraNome,
                TipoCarga = tipoCarga,
                StatusMovimentacao = result.Status.GetDescription(),
                DataHoraEntrada = result.DataHoraEntrada,
                DataHoraSaida = result.DataHoraSaida,
                TempoPermanencia = $"{result.TempoPermanenciaMinutos} min",
                Patio = string.Empty,
                Observacao = !string.IsNullOrWhiteSpace(result.Observacao)
                    ? result.Observacao
                    : (result.Descricao ?? string.Empty)
            };
        }

        private async Task TratamentoEntradaSaida(EntradaPostInput input, EntradaSaida result)
        {
            var transportadoraId = await ResolverTransportadoraId(input.Transportadora);
            var motoristaId = await ResolverMotoristaId(input.Motorista);
            var veiculoId = await ResolverVeiculoId(input.Veiculo, transportadoraId);
            transportadoraId ??= await ObterTransportadoraDoVeiculo(veiculoId);

            result.TransportadoraId = transportadoraId;
            result.MotoristaId = motoristaId;
            result.VeiculoId = veiculoId;
            result.DataHoraEntrada = input.DataHoraEntrada ?? DateTime.Now;
            result.DataHoraUltimaEntradaPatio = result.DataHoraEntrada;
            result.PermanenciaSuspensa = false;
            result.Finalizado = false;
            result.TempoPermanenciaMinutos = 0;
            result.TempoTotalSuspensaoMinutos = 0;
            result.UsuarioRegistroEntradaId = _currentUser.Id;
            result.UsuarioRegistroEntradaNome = _currentUser.Name;
            result.Descricao = $"{input.Veiculo?.Placa} - {input.Motorista?.Nome}";
            result.Status = input.DataHoraEntrada.HasValue ? EntradaSaidaStatus.Entrada : EntradaSaidaStatus.Agendado;
        }

    }
}
