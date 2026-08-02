using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estac.Infra.Repositories
{
    public class PessoaContatoRepositories : IPessoaContatoRepositories
    {
        private readonly GtsContext _context;

        public PessoaContatoRepositories(GtsContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PessoaContato>> ListarPorPessoaIdAsync(int pessoaId)
        {
            return await _context.Set<PessoaContato>()
                .AsNoTracking()
                .Where(x => x.PessoaId == pessoaId)
                .ToListAsync();
        }

        public async Task AtualizarContatos(int pessoaId, IEnumerable<PessoaContato> contatos)
        {
            var lista = contatos ?? Enumerable.Empty<PessoaContato>();

            var existentes = await _context.Set<PessoaContato>()
                .Where(x => x.PessoaId == pessoaId)
                .ToListAsync();

            if (existentes.Count > 0)
                _context.RemoveRange(existentes);

            foreach (var c in lista)
            {
                c.Id = 0;
                c.PessoaId = pessoaId;
                c.Pessoa = null!;
                await _context.AddAsync(c);
            }
        }
    }
}
