namespace Estac.Domain.Output.Veiculo
{
    /// <summary>Vínculo motorista na listagem de veículos (GET Buscar).</summary>
    public class VeiculoMotoristaSearchOutput
    {
        public int Id { get; set; }
        /// <summary>Descrição do motorista (<c>Motorista.Descricao</c>).</summary>
        public string Motorista { get; set; }
        /// <summary>CPF do motorista (<c>Pessoa.Documento</c>).</summary>
        public string Cpf { get; set; }
        /// <summary>Flag principal do vínculo (<c>VeiculoMotorista.Principal</c>).</summary>
        public bool? Principal { get; set; }
    }
}
