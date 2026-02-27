
using System.Web;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraFilterInput : FilterInput
    {
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Cnpj { get; set; }
    }
}