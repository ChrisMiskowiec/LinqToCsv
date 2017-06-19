using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToCsv
{
    public class QueryProvider : IQueryProvider
    {
        private readonly IQueryContext _queryContext;

        public QueryProvider(IQueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(Queryable<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Queryable<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return _queryContext.Execute(expression, false);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)_queryContext.Execute(expression, (typeof(TResult).Name == "IEnumerable`1"));
        }
    }
}
