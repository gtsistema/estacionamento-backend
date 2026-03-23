namespace Estac.Domain.Input.Auth
{
    public class PermissionInput
    {
        public int Ordem { get; set; }
        public int Id { get; set; }
        public int SubModuleId { get; set; }
        public string Descricao { get; set; }
    }
}