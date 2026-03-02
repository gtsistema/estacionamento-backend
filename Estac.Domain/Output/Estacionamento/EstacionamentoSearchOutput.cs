using Estac.Domain.Models.Enuns;
using System.Web;

namespace Estac.Domain.Output.Estacionamento
{
    public class EstacionamentoSearchOutput
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Descricao { get; set; }
        public string Documento { get; set; }
        public bool Ativo { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public TipoPessoa Tipo { get; set; }
        public string Email { get; set; }
    }
}