namespace Estac.Domain.Output.Auth
{
    public class PerfilOutput
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public List<MenuOutput> Menus { get; set; } = new List<MenuOutput>();
    }
}
