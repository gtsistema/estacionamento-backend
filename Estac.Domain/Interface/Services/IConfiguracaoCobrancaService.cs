using Estac.Domain.Input.ConfiguracaoCobranca;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IConfiguracaoCobrancaService
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(ConfiguracaoCobrancaFilterInput filter);
        Task<ActionResult> Gravar(ConfiguracaoCobrancaPostInput input);
        Task<ActionResult> Alterar(ConfiguracaoCobrancaPutInput input);
        Task<ActionResult> Excluir(int id);
    }
}
