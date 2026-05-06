using Estac.Domain.Models;
using System.Linq;

namespace Estac.Domain.Extensions
{
    public static class PessoaExtensions
    {
        /// <summary>
        /// E-mail do contato marcado como principal; se não houver, o primeiro contato com e-mail preenchido.
        /// </summary>
        public static string ObtemEmailPrincipal(this Pessoa pessoa)
        {
            if (pessoa?.Contatos == null || !pessoa.Contatos.Any())
                return null;

            return pessoa.Contatos
                .Where(c => c != null && !string.IsNullOrWhiteSpace(c.Email))
                .OrderByDescending(c => c.Principal)
                .Select(c => c.Email)
                .FirstOrDefault();
        }
    }
}
