using Estac.Domain.Models.Base;
using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Models
{
    public class ContaBancaria : BaseInt
    {
        public int EstacionamentoId { get; set; }
        public int? TransportadoraId { get; set; }
        public string Titular { get; private set; }
        public string CpfCnpj { get; private set; }
        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string AgenciaDigito { get; private set; }
        public string Conta { get; private set; }
        public string ContaDigito { get; private set; }
        public string TipoConta { get; private set; }
        public bool Ativa { get; private set; }
        public string ChavePix { get; private set; }
        public TipoChave? TipoChave { get; private set; }
        public Estacionamento Estacionamento { get; set; }
        public Transportadora Transportadora { get; set; }
    }
}