using Estac.Domain.Input.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services.Auth
{
    public interface IUserServices
    {
        Task<ActionResult> LoginAsync(LoginInput dto);
        Task<ActionResult> ObterTokenAsync(ObterTokenInput dto);
        Task<ActionResult> RegisterAsync(RegisterInput dto);
        Task<ActionResult> Buscar();
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Alterar(int id, RegisterInput input);
        Task<ActionResult> Delete(int id);
        Task<ActionResult> ConfirmarEmailAsync(ConfirmarEmailInput input);
        Task<ActionResult> EsqueciSenhaAsync(EsqueciSenhaInput input);
        Task<ActionResult> RedefinirSenhaAsync(RedefinirSenhaInput input);
    }
}
