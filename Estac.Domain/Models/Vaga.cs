using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Vaga : BaseInt
    {
        public bool Ativo { get; set; }
        public string Observacao { get; set; }
        public ICollection<VagaVeiculo> VagaVeiculos { get; set; }
    }
}