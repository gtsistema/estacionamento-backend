namespace Estac.Domain.Input.Estacionamento
{
    /// <summary>
    /// Payload do front para gravar/alterar fuso.
    /// O dropdown envia apenas o <see cref="TimeZoneId"/> selecionado.
    /// </summary>
    public class EstacionamentoConfiguracaoPostInput
    {
        /// <summary>
        /// Valor do dropdown (IANA). Exemplos: America/Cuiaba, America/Sao_Paulo.
        /// Deve ser um dos itens retornados em GET /api/EstacionamentoConfiguracao/padroes.
        /// </summary>
        public string TimeZoneId { get; set; }
    }

    public class EstacionamentoConfiguracaoPutInput : EstacionamentoConfiguracaoPostInput
    {
        /// <summary>Id da configuração já existente (retornado no GET).</summary>
        public int Id { get; set; }
    }
}
