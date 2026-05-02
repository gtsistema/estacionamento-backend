using Estac.Domain.Input.Base;

namespace Estac.Domain.Input.ContaBancaria
{
    public class ContaBancariaInput : BaseIntInput
    {
        public int Id { get; set; }
        public int EstacionamentoId { get; set; }
        public string Titular { get; set; }
        public string CpfCnpj { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDigito { get; set; }
        public string Conta { get; set; }
        public string ContaDigito { get; set; }
        public string TipoConta { get; set; }
        public bool Ativa { get; set; }
        public string ChavePix { get; set; }
    }
}
