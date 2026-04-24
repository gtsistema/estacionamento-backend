using Estac.Domain.Input.Auth;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;

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
        Task AtualizarPermissao(Models.Auth.Permission permission);
        Task GravarSubMenu(SubModule subModule);
        Task AtualizarSubMenu(SubModule subModule);
        Task DeletarSubMenu(SubModule subModule);
        Task Deletar(Module module);
        Task DeletarPermissao(Models.Auth.Permission subModule);
        Task GravarPermissao(Models.Auth.Permission permission);
        Task<Models.Auth.Permission> SelecionarPermissaoPorId(int id);
        Task<SubModule> SelecionarSubModulePorId(int id);
    }
}