namespace Estac.Domain.Input.Auth
{
    public class MenuCreateInput
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<SubMenuCreateInput> SubMenus { get; set; }
    }
}
