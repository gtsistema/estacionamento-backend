using System.Linq.Expressions;

namespace Estac.Domain.Extensions
{
    public class OrdernableEntity<T> where T : class
    {
        public Expression<Func<T, object>> Expression { get; private set; }

        public bool Ascending { get; private set; }

        public static OrdernableEntity<T> GenerateOrderByExpression(Expression<Func<T, object>> expression, bool ascending = true)
        {
            return new OrdernableEntity<T>
            {
                Expression = expression,
                Ascending = ascending
            };
        }
    }
}
