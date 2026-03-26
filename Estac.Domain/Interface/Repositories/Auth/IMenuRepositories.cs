using Estac.Domain.Input.Auth;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;
using Estac.Domain.Shared;

namespace Estac.Domain.Interface.Repositories
{
    public interface IMenuRepositories : IBaseRepositoriesIdentityNone<Module>
    {
        Task<Module> SelecionarPorIdCompleto(int id);
        Task<Module> SelecionarPorId(int id);
        Task<List<Module>> Buscar();
        Task AtualizarOrdem(List<MenuOrdemInput> menus, List<SubMenuOrdemInput> subMenus);
        Task<List<MenuAcessOuput>> BuscarMenuUsuario(int roleId);
        Task Atualizar(Module menu);
        Task AtualizarPermissao(Permission permission);
        Task GravarSubMenu(SubModule subModule);
        Task AtualizarSubMenu(SubModule subModule);
        Task DeletarSubMenu(IEnumerable<SubModule> subModule);
        Task Deletar(Module module);
        Task GravarPermissao(Permission permission);
    }
}