using Estac.Domain.Models.Base;

namespace Estac.Domain.Models.Auth
{
    public class Module : BaseIntDataNull
    {
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public string Rota { get; set; }
        public ICollection<SubModule> SubModules { get; set; }
    }
}