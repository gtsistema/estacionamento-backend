
namespace Estac.Domain.Input.Auth
{
    public class PerfilCreateInput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<ModuloInput> Menus { get; set; }
    }
}
