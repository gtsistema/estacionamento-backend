namespace Estac.Domain.Dto.Perfil
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public string Rota { get; set; }
        public bool Selecionado { get; set; }
        public List<SubMenuDto> SubMenus { get; set; }
    }
}
