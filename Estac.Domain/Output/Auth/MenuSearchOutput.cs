using Estac.Domain.Models.Enuns;
using System.Web;

namespace Estac.Domain.Output.Auth
{
    public class MenuSearchOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public List<SubMenuOutput> SubModules { get; set; }
    }
}
