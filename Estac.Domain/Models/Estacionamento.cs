using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    public class Estacionamento : BaseIntDataNull
    {
        public int PessoaId { get; set; }
        public int? CapacidadeVeiculo { get; set; }
        public bool Tamanho { get; set; }
        public bool? PossuiSeguranca { get; set; }
        public bool? PossuiBanheiro { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}