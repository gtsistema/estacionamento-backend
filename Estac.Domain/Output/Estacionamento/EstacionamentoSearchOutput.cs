namespace Estac.Domain.Output.Estacionamento
{
    public class EstacionamentoSearchOutput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int PessoaId { get; set; }
        public string CNPJ { get; set; }    
    }
}