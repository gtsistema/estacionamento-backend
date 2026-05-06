using Estac.Domain.Output.Auth;
using Estac.Domain.Output.Auth.Usuario;

namespace Estac.Domain.Interface.Repositories.Auth
{
    public interface IUsuarioRepositories
    {
        Task<IEnumerable<UsuarioOutput>> BuscarUsuariosGrid(int? usuarioId);
        Task<UsuarioCadastroOutput> SelecionarUsuarioPorId(int id);
    }
}