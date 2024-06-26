using Gp.Domain.Models;
using Gp.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gp.Domain.Extensions
{
   
    public static class QueryableExtesions
    {
        public static async Task<PagedQuery<T>> ToPagedQueryAsync<T>(this IOrderedQueryable<T> query, int page, int pageSize)
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

        public static async Task<IOrderedQueryable<T>> ToPagedSortAsync<T>(this IQueryable<T> query, string sort, string propertyName)
                         where T : class
        {
            return await Task.Run(() =>
            {
                var sortDirection = sort.ToLower().Contains("desc") ? "Descending" : "Ascending";

                var param = Expression.Parameter(typeof(T), "p");
                var property = Expression.Property(param, propertyName);
                var sortExpression = Expression.Lambda(property, param);

                var methodName = sortDirection == "Descending" ? "OrderByDescending" : "OrderBy";
                var method = typeof(Queryable).GetMethods()
                                              .First(m => m.Name == methodName && m.GetParameters().Length == 2);

                var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);

                return (IOrderedQueryable<T>)genericMethod.Invoke(null, new object[] { query, sortExpression });
            });
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
