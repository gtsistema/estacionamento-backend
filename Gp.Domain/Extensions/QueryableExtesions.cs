using Gp.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Gp.Domain.Extensions
{
   
    public static class QueryableExtesions
    {
        public static async Task<PagedQuery<T>> ToPagedQuery<T>(this IOrderedQueryable<T> query, int page, int pageSize)
                where T : class
        {
            var result = new PagedQuery<T>
            {
                PaginaAtual = page,
                TamanhoPagina = pageSize,
            };


            var skip = (page - 1) * pageSize;
            result.Dados = await query.Skip(skip).Take(pageSize).ToListAsync();
            result.PaginaTotal = result.Dados.Count();

            return result;
        }
    }

    public static class EnumerableExtesions
    {
        public static PagedQuery<T> ToPagedQuery<T>(this IEnumerable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new PagedQuery<T>
            {
                PaginaAtual = page,
                TamanhoPagina = pageSize,
                PaginaTotal = query.Count()
            };


            var skip = (page - 1) * pageSize;
            result.Dados = query.Skip(skip).Take(pageSize).ToList();
            result.PaginaTotal = result.Dados.Count();

            return result;
        }
    }
}
