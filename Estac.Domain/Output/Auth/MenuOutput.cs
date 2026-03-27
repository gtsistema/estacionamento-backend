namespace Estac.Domain.Output.Auth
{
    public class MenuOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get;set; }
        public List<SubMenuOutput> SubMenus { get; set; }
    }
}