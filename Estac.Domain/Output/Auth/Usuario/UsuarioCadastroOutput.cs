
namespace Estac.Domain.Output.Auth.Usuario
{
    public class UsuarioCadastroOutput
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int? EmpresaId { get; set; }
        public string Empresa { get; set; }
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
    }
}
