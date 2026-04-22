using Estac.Domain.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estac.Domain.Models.Auth
{
    public class Module : BaseIntDataNull
    {
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public string Rota { get; set; }
        [NotMapped]
        public bool Selecionado { get; set; }
        public ICollection<SubModule> SubModules { get; set; }
    }
}