using Estac.Domain.Auth;
using Estac.Domain.Input.Auth;
using Estac.Domain.Output.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services.Auth
{
    public interface IPerfilServices
    {
        Task<ActionResult> Listar();

        Task<ActionResult> ObterPorId(Guid id);

        Task<ActionResult> Gravar(ApplicationRole input);

        Task<ActionResult> Alterar(ApplicationRole input);

        Task<ActionResult> Delete(Guid id);
    }
}
