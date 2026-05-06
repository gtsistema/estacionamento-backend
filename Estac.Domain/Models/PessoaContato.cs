
using Estac.Domain.Models.Base;

namespace Estac.Domain.Models
{
    /// <summary>
    /// Contato vinculado à pessoa (nome/descrição do contato, CPF, telefone, e-mail, principal e observações).
    /// <see cref="BaseIntDataNull.Descricao"/> identifica o contato (ex.: "Financeiro", "João").
    /// </summary>
    public class PessoaContato : BaseIntDataNull
    {
        public int PessoaId { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Principal { get; set; }
        public string Observacao { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
