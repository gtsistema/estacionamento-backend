using Estac.Domain.Models.Enuns;

namespace Estac.Domain.Output.Motorista
{
    public class EntradaSaidaVinculoOutput
    {
        public int? Id { get; set; }
        public bool ExisteEntradaEmAberto { get; set; }
        public DateTime? DataHoraEntrada { get; set; }
        public string Observacao { get; set; }
        public EntradaSaidaStatus? Status { get; set; }
        public int? VeiculoId { get; set; }
        public string Placa { get; set; }
        public TipoCarga? TipoCarga { get; set; }
        public int? TransportadoraId { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }
        public EntradaSaidaMotoristaVinculoOutput Motorista { get; set; }
    }

    public class EntradaSaidaMotoristaVinculoOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool? Principal { get; set; }
    }
}
