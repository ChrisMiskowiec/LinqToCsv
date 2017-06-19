using System;
using System.IO;
using System.Linq.Expressions;

namespace LinqToCsv
{
    public class CsvQueryContext : IQueryContext, IDisposable
    {
        private Stream _stream;

        public CsvQueryContext(string csvFile)
        {
            _stream = File.OpenRead(csvFile);
        }

        public CsvQueryContext(Stream stream)
        {
            _stream = stream;
        }

        public object Execute(Expression expression, bool isEnumerable)
        {
            if (isEnumerable)
            {
                return null;
            }
            return null;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}