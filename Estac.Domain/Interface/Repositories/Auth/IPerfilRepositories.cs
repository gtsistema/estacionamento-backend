using Estac.Domain.Output.Auth;

namespace Estac.Domain.Interface.Repositories.Auth
{
    public interface IPerfilRepositories
    {
        Task<UsuarioAcessoOutput> BuscarPerfilPermissaoUsuario(int usuarioId);
    }
}
