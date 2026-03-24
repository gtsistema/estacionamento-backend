using Estac.Domain.Input.Auth;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IMenuRepositories : IBaseRepositoriesNone<Module>
    {
        Task<Module> SelecionarPorIdCompleto(int id);
        Task<List<Module>> Buscar();
        Task AtualizarOrdem(List<MenuOrdemInput> menus, List<SubMenuOrdemInput> subMenus);
        Task<List<MenuAcessOuput>> BuscarMenuUsuario(int roleId);
    }
}