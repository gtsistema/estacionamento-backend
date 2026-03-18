using Estac.Domain.Models.Base;

namespace Estac.Domain.Models.Auth
{
    public class SubModule : BaseIntDataNull
    {
        public int Ordem { get; set; }
        public int ModuleId { get; set; }  
        public string Rota { get; set; }
        public bool Ativo { get; set; }
        public Module Module { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}