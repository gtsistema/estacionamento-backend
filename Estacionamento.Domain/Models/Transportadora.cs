using Estac.Domain.Models.Base;
namespace Estac.Domain.Models
{
    public class Transportadora : BaseIntDataNull
    {
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public ICollection<Veiculo> Veiculos { get; set; }
        public ICollection<ContaBancaria> ContasBancarias { get; set; }
    }
}