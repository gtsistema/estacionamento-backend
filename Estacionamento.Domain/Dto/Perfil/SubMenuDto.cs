
namespace Estac.Domain.Dto.Perfil
{
    public class SubMenuDto
    {
        public int SubMenuId { get; set; }
        public string SubDescricao { get; set; }
        public int SubOrdem { get; set; }
        public bool SubAtivo { get; set; }
        public string SubRota { get; set; }
        public bool SubSelecionado { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}