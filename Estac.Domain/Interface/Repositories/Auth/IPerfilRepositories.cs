using Estac.Domain.Dto.Perfil;
using Estac.Domain.Models.Auth;
using Estac.Domain.Output.Auth;

namespace Estac.Domain.Interface.Repositories.Auth
{
    public interface IPerfilRepositories
    {
        Task<ApplicationRole> BuscarPerfilPorId(int roleId);
        Task<UsuarioRoleOuput> BuscarPerfilPorUsuarioToken(int usuarioId);
        Task<UsuarioAcessoPerfilOutput> BuscarPerfilPermissaoUsuario(int usuarioId);
        Task AdicionarPermissoesPerfil(IEnumerable<RolePermission> rolePermissions);
        Task<PerfilOutput> BuscarTodosPerfilMenu(int perfilId);
        Task<IEnumerable<RolePermission>> BuscarRolePermissoes(int perfilId);
        Task AtualizarPermissoesDoPerfil(IEnumerable<RolePermission> rolePermissionsGravar, IEnumerable<RolePermission> rolePermissionsAtuais);
        Task AdicionarPerfilSimples(ApplicationRole role);
        void RemoveTodasPermissoesDoPerfil(IEnumerable<RolePermission> rolePermissions);
        Task AtualizarPerfilSimplesAsync(ApplicationRole role);
        Task<IEnumerable<PerfilDto>> BuscarPerfilPermissaoGrid(int? roleId);
        Task<Models.Auth.Permission> BuscarPermissao(int subSubMenuId, string acao);
    }
}
