using System.ComponentModel.DataAnnotations.Schema;

namespace Estac.Domain.Models.Auth
{
    public class Permission
    {
        public int Ordem { get; set; }
        public int Id { get; set; }
        public int SubModuleId { get; set; }
        public string Acao { get; set; }
        [NotMapped]
        public bool SelecionadoPerm { get; set; }   
        public SubModule SubModule { get; set; }
    }
}