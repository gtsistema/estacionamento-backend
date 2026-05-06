using Estac.Domain.Models;
using System.Collections.Generic;

namespace Estac.Domain.Interface.Repositories
{
    public interface IPessoaContatoRepositories
    {
        Task<IReadOnlyList<PessoaContato>> ListarPorPessoaIdAsync(int pessoaId);

        /// <summary>Remove todos os contatos da pessoa e insere a lista informada (Ids zerados).</summary>
        Task AtualizarContatos(int pessoaId, IEnumerable<PessoaContato> contatos);
    }
}
