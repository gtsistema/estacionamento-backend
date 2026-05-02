using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class PessoaEnderecoRepositories : IPessoaEnderecoRepositories
    {
        private readonly GtsContext _context;

        public PessoaEnderecoRepositories(GtsContext context)
        {
            _context = context;
        }

        public async Task AtualizarEndereco(int pessoaId, IEnumerable<PessoaEndereco> enderecos)
        {
            var lista = enderecos ?? Enumerable.Empty<PessoaEndereco>();

            var existentes = await _context.Set<PessoaEndereco>()
                .Where(x => x.PessoaId == pessoaId)
                .ToListAsync();

            if (existentes.Count > 0)
                _context.RemoveRange(existentes);

            foreach (var e in lista)
            {
                e.Id = 0;
                e.PessoaId = pessoaId;
                e.Pessoa = null!;
                await _context.AddAsync(e);
            }
        }
    }
}
