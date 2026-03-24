using Estac.Domain.Input.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estac.Domain.Interface.Services
{
    public interface IMenuServices
    {
        Task<ActionResult> ObterPorId(int id);
        Task<ActionResult> Buscar();
        Task<ActionResult> Gravar(MenuCreateInput input);
        Task<ActionResult> Alterar(MenuCreateInput input);
        Task<ActionResult> Excluir(int id);
        Task<ActionResult> OrganizarMenus(MenuOrganizacaoInput input);
    }
}