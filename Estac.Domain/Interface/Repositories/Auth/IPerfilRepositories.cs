using Estac.Domain.Output.Auth;

namespace Estac.Domain.Interface.Repositories.Auth
{
    public interface IPerfilRepositories
    {
        Task<UsuarioRoleOuput> BuscarPerfilPorUsuarioToken(int usuarioId);
        Task<UsuarioAcessoPerfilOutput> BuscarPerfilPermissaoUsuario(int usuarioId);
    }
}
