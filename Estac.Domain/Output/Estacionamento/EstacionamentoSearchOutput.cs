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
        /// <summary>Descrição da pessoa (ex.: nome fantasia), vinda de <c>Pessoa.Descricao</c>.</summary>
        public string DescricaoPessoa { get; set; }
        public TipoPessoa Tipo { get; set; }
        public string Email { get; set; }
        public string ResponsavelLegal { get; set; }
        public string ResponsavelCpf { get; set; }
        public string ResponsavelEmail { get; set; }
        public string ResponsavelTelefone { get; set; }
    }
}