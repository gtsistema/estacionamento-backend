namespace Estac.Domain.Output.Auth
{
    public class SubMenuOutput
    {
        public int Id { get; set; } 
        public string Descricao { get; set; }
        public int Ordem { get; set; }  
        public List<PermissionOutput> Permissions { get; set; }
    }
}