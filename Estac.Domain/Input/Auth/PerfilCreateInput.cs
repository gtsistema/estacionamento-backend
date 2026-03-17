
namespace Estac.Domain.Input.Auth
{
    public class PerfilCreateInput
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<ModuloInput> Modulos { get; set; }
    }
}
