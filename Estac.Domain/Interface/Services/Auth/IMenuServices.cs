using Estac.Domain.Input.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Estac.Domain.Interface.Services
{
    public interface IMenuServices
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar(MenuFilterInput filter);
        Task<ActionResult> Gravar(MenuCreateInput input);
        Task<ActionResult> Alterar(MenuCreateInput input);
        Task<ActionResult> Excluir(int id);
    }
}