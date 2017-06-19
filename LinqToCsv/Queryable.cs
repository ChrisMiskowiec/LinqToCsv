using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv
{
    public class Queryable<T> : IOrderedQueryable<T>
    {
        private readonly Expression _expression;
        public Expression Expression => _expression;

        public Type ElementType => typeof(T);

        private readonly IQueryProvider _provider;
        public IQueryProvider Provider => _provider;

        private readonly IQueryContext _queryContext;

        public Queryable(IQueryContext queryContext)
            : this(new QueryProvider(queryContext), null)
        {
        }

        public Queryable(IQueryProvider provider)
            : this(provider, null)
        {
        }

        public Queryable(IQueryProvider provider, Expression expression)
        {
            _provider = provider;
            _expression = expression ?? Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }
}
