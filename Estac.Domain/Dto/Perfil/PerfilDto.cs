
namespace Estac.Domain.Dto.Perfil
{
    public class PerfilDto
    {
        public string PerfilId { get; set; }
        public string Perfil { get; set; }
        public List<MenuDto> Menus { get; set; } = new List<MenuDto>();
    }
}