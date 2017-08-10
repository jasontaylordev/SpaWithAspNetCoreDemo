using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Tests.Infrastructure;
using Northwind.Domain;
using Northwind.Persistance;
using Xunit;

namespace Northwind.Application.Tests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerDetailQueryTests
    {
        private readonly NorthwindContext _context;

        public GetCustomerDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task ShouldReturnNullGivenInvalidId()
        {
            var query = new GetCustomerDetailQuery(_context);

            var result = await query.Execute("ABCDE");

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnCustomerDetailGivenValidId()
        {
            var query = new GetCustomerDetailQuery(_context);

            var result = await query.Execute("JASON");

            Assert.NotNull(result);
            Assert.Equal("JASON", result.Id);
            Assert.Equal("Jason Taylor", result.ContactName);
        }
    }
}