using AutoMapper;
using Estac.Domain.Input.Estacionamento;
using Estac.Domain.Interface.Repositories;
using Estac.Domain.Interface.Services;
using Estac.Domain.Models;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output;
using Estac.Domain.Output.Estacionamento;
using Estac.Domain.Shared;
using Estac.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Service
{
    public class EstacionamentoConfiguracaoService : ServiceResult<EstacionamentoConfiguracaoOutput>, IEstacionamentoConfiguracaoService
    {
        private readonly IEstacionamentoConfiguracaoRepositories _repositories;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public EstacionamentoConfiguracaoService(
            IErrorServices errorServices,
            IEstacionamentoConfiguracaoRepositories repositories,
            ICurrentUser currentUser,
            IMapper mapper) : base(errorServices)
        {
            _repositories = repositories;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public Task<ActionResult> ListarPadroesBrasil()
        {
            var padroes = FusoHorarioBrasilPadroes.Todos
                .Select(p => new FusoHorarioPadraoOutput
                {
                    TimeZoneId = p.TimeZoneId,
                    Nome = p.Nome,
                    UtcOffset = p.UtcOffset
                })
                .OrderBy(p => p.Nome)
                .ToList();

            return RetornOk(padroes);
        }

        public async Task<ActionResult> ObterDoUsuario()
        {
            return await ObterPorEstacionamentoId(ObterEstacionamentoIdDoUsuario());
        }

        public async Task<ActionResult> ObterPorEstacionamentoId(int estacionamentoId)
        {
            if (estacionamentoId <= 0)
                return await RetornNo(false, "EstacionamentoId inválido.");

            var entity = await _repositories.ObterPorEstacionamentoIdAsync(estacionamentoId);
            if (entity is null)
                return await RetornNo(false, "Configuração do estacionamento não encontrada.", statusCode: 404);

            return await RetornOk(MapearOutput(entity));
        }

        public async Task<ActionResult> Gravar(EstacionamentoConfiguracaoPostInput input)
        {
            if (input is null)
                return await RetornNo(false, "Dados de configuração são obrigatórios.");

            var estacionamentoId = ObterEstacionamentoIdDoUsuario();

            if (await _repositories.ExistePorEstacionamentoIdAsync(estacionamentoId))
                return await RetornNo(false, "Já existe configuração para este estacionamento. Use PUT para alterar.");

            var erro = ValidarTimeZone(input.TimeZoneId);
            if (erro != null)
                return await RetornNo(false, erro);

            var entity = new EstacionamentoConfiguracao
            {
                EstacionamentoId = estacionamentoId,
                TimeZoneId = input.TimeZoneId.Trim(),
                Cultura = "pt-BR",
                Ativo = true
            };

            var salvo = await _repositories.GravarAsync(entity);
            return await RetornOk(MapearOutput(salvo));
        }

        public async Task<ActionResult> Alterar(EstacionamentoConfiguracaoPutInput input)
        {
            if (input is null || input.Id <= 0)
                return await RetornNo(false, "Id da configuração é obrigatório para alteração.");

            var existente = await _repositories.ObterPorIdAsync(input.Id);
            if (existente is null)
                return await RetornNo(false, "Configuração do estacionamento não encontrada.", statusCode: 404);

            if (existente.EstacionamentoId != ObterEstacionamentoIdDoUsuario())
                return await RetornNo(false, "Configuração não pertence ao estacionamento do usuário logado.");

            var erro = ValidarTimeZone(input.TimeZoneId);
            if (erro != null)
                return await RetornNo(false, erro);

            existente.TimeZoneId = input.TimeZoneId.Trim();
            existente.Cultura = "pt-BR";
            existente.Ativo = true;

            var atualizado = await _repositories.AlterarAsync(existente);
            return await RetornOk(MapearOutput(atualizado));
        }

        private static string ValidarTimeZone(string timeZoneId)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                return "timeZoneId é obrigatório. Selecione um item do dropdown (GET /padroes).";

            var id = timeZoneId.Trim();
            var existeNoCatalogo = FusoHorarioBrasilPadroes.Todos
                .Any(p => string.Equals(p.TimeZoneId, id, StringComparison.OrdinalIgnoreCase));

            if (!existeNoCatalogo)
                return $"timeZoneId inválido: {id}. Use um valor retornado em GET /api/EstacionamentoConfiguracao/padroes.";

            if (!TimeZoneHelper.IsValid(id))
                return $"Fuso horário não suportado pelo servidor: {id}.";

            return null;
        }

        private EstacionamentoConfiguracaoOutput MapearOutput(EstacionamentoConfiguracao entity)
        {
            var output = _mapper.Map<EstacionamentoConfiguracaoOutput>(entity);
            var padrao = FusoHorarioBrasilPadroes.Todos
                .FirstOrDefault(p => string.Equals(p.TimeZoneId, entity.TimeZoneId, StringComparison.OrdinalIgnoreCase));

            output.Nome = !string.IsNullOrWhiteSpace(padrao.Nome) ? padrao.Nome : entity.TimeZoneId;
            output.UtcOffset = !string.IsNullOrWhiteSpace(padrao.UtcOffset)
                ? padrao.UtcOffset
                : TimeZoneHelper.FormatOffset(entity.TimeZoneId);

            return output;
        }

        private int ObterEstacionamentoIdDoUsuario()
        {
            if (_currentUser.EmpresaId <= 0)
                throw new InvalidOperationException("Usuário logado sem estacionamento vinculado.");
            return _currentUser.EmpresaId;
        }
    }
}
