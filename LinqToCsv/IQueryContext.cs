using System.Linq.Expressions;

namespace LinqToCsv
{
    public interface IQueryContext
    {
        object Execute(Expression expression, bool isEnumerable);
    }
}
