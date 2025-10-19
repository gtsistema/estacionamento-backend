using AutoMapper;
using Estac.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using LinqKit;

namespace Estac.Domain.Extensions
{

    public static class QueryableExtesions
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            PagedResult<T> obj = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.AsNoTracking().Count()
            };
            double a = (double)obj.RowCount / (double)pageSize;
            obj.PageCount = (int)Math.Ceiling(a);
            int count = (page - 1) * pageSize;
            obj.Results = query.AsNoTracking().Skip(count).Take(pageSize)
                .ToList();
            return await Task.FromResult(obj);
        }

        public static IQueryable<T> ManyWhere<T>(this IQueryable<T> query, IQueryable<Expression<Func<T, bool>>> predicate)
        {
            foreach (Expression<Func<T, bool>> item in predicate)
            {
                query = query.Where(item.Expand());
            }

            return query;
        }

        public static IEnumerable<T> ManyWhere<T>(this IEnumerable<T> query, IEnumerable<Expression<Func<T, bool>>> predicate)
        {
            foreach (Expression<Func<T, bool>> item in predicate)
            {
                query = query.AsQueryable().Where(item.Expand());
            }

            return query;
        }
        
        public static IEnumerable<T> ManyOrderBy<T>(this IEnumerable<T> query, IEnumerable<OrdernableEntity<T>> predicate) where T : class
        {
            foreach (OrdernableEntity<T> item in predicate)
            {
                query = (item.Ascending ? query.AsQueryable().OrderBy(item.Expression.Expand()) : query.AsQueryable().OrderByDescending(item.Expression.Expand()));
            }

            return query;
        }

        public static IEnumerable<T> ManyOrderBy<T>(this IEnumerable<T> query, IEnumerable<Expression<Func<T, object>>> predicate, bool asc = true)
        {
            foreach (Expression<Func<T, object>> item in predicate)
            {
                query = (asc ? query.AsQueryable().OrderBy(item.Expand()) : query.AsQueryable().OrderByDescending(item.Expand()));
            }

            return query;
        }

        public static async Task<IOrderedQueryable<T>> ToPagedSort<T>(this IQueryable<T> query, string sort, string propertyName)
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
}
