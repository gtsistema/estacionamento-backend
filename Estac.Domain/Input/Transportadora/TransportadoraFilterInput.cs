
using System.Web;

namespace Estac.Domain.Input.Transportadora
{
    public class TransportadoraFilterInput : FilterInput
    {
        public string RazaoSocial { get; set; }
        /// <summary>Filtro pela descrição (nome fantasia) da pessoa vinculada.</summary>
        public string DescricaoPessoa { get; set; }
        public string Cnpj { get; set; }
    }
}