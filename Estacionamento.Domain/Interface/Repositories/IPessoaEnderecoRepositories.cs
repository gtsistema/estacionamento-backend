using Estac.Domain.Models;

namespace Estac.Domain.Interface.Repositories
{
    public interface IPessoaEnderecoRepositories
    {
        /// <summary>Remove todos os endereços da pessoa e insere a lista informada (Ids zerados).</summary>
        Task AtualizarEndereco(int pessoaId, IEnumerable<PessoaEndereco> enderecos);
    }
}
