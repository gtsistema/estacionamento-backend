using Estac.Domain.Output.Auth;

namespace Estac.Domain.Interface.Repositories.Auth
{
    public interface IUsuarioRepositories
    {
        Task<IEnumerable<UsuarioOutput>> BuscarUsuariosGrid(int? usuarioId);
    }
}
