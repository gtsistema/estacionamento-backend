using Estac.Domain.Models.Base;

namespace Estac.Domain.Models.Auth
{
    public class Module : BaseIntDataNull
    {
        public int Ordem { get; set; }
        public ICollection<SubModule> SubModules { get; set; }
    }
}