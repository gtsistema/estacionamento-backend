using Estac.Domain.Input.Pessoa;

namespace Estac.Domain.Input.Motorista
{
    public class MotoristaPostInput
    {
        public int Id { get; set; }
        public string CNH { get; set; }
        public DateTime? ValidadeCNH { get; set; }
        public int PessoaId { get; set; }
        public PessoaMotoristaInput PessoaFisica { get; set; }
    }
}
