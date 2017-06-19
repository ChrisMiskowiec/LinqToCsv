using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Tests
{
    [TestFixture]
    public class QueryProviderTests
    {
        [Test]
        public void Execute()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(
                @"Name,Age,Gender
                  Bob,32,Male
                  Jane,25,Female
                  Ted,19,Male"));
            var query = new Queryable<Person>(new CsvQueryContext(stream));
            var results = query.Where(p => p.Age > 20).Count();

            Assert.That(results, Is.EqualTo(2));
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
        }
    }
}
