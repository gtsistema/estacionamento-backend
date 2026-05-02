using Estac.Domain.Models;

namespace Estac.Domain.Interface.Repositories
{
    public interface IPessoaContatoRepositories
    {
        /// <summary>Remove todos os contatos da pessoa e insere a lista informada (Ids zerados).</summary>
        Task AtualizarContatos(int pessoaId, IEnumerable<PessoaContato> contatos);
    }
}
