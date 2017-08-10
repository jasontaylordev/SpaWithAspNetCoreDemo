using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Domain;
using Northwind.Persistance;
using Xunit;

namespace Northwind.Application.Tests
{
    public class GetCustomerDetailQueryTests
    {
        [Fact]
        public async Task ShouldReturnNullGivenInvalidId()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase("ShouldReturnNullGivenInvalidId")
                .Options;

            var context = new NorthwindContext(options);

            context.Customers.AddRange(new[] {
                new Customer { CustomerId = "ADAM", ContactName = "Adam Cogan" },
                new Customer { CustomerId = "JASON", ContactName = "Jason Taylor" },
                new Customer { CustomerId = "BREND", ContactName = "Brendan Richards" },
            });

            context.SaveChanges();

            var query = new GetCustomerDetailQuery(context);

            var result = await query.Execute("ABCDE");

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnCustomerDetailGivenValidId()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseInMemoryDatabase("ShouldReturnCustomerDetailGivenValidId")
                    .Options;

            var context = new NorthwindContext(options);

            context.Customers.AddRange(new[] {
                new Customer { CustomerId = "ADAM", ContactName = "Adam Cogan" },
                new Customer { CustomerId = "JASON", ContactName = "Jason Taylor" },
                new Customer { CustomerId = "BREND", ContactName = "Brendan Richards" },
            });

            context.SaveChanges();

            var query = new GetCustomerDetailQuery(context);

            var result = await query.Execute("JASON");

            Assert.NotNull(result);
            Assert.Equal("JASON", result.Id);
            Assert.Equal("Jason Taylor", result.ContactName);
        }
    }
}