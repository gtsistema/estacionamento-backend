
namespace Estac.Domain.Dto.Perfil
{
    public class PermissionDto
    {
        public int PermissaoId { get; set; }
        public int PermissaoOrdem { get; set; }
        public string Acao { get; set; }
        public bool PermSelecionado { get; set; }
    }
}
