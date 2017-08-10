using System;
using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using Northwind.Persistance;
using Xunit;

namespace Northwind.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public NorthwindContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = NorthwindContextFactory.Create();
        }

        public void Dispose()
        {
            NorthwindContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}