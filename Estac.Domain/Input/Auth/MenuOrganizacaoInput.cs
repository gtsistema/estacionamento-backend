namespace Estac.Domain.Input.Auth
{
    public class MenuOrganizacaoInput
    {
        public List<MenuOrdemInput> Menus { get; set; } 
    }

    public class MenuOrdemInput
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
        public List<SubMenuOrdemInput> SubMenus { get; set; }
    }

    public class SubMenuOrdemInput
    {
        public int Id { get; set; }
        public int Ordem { get; set; }
    }
}
