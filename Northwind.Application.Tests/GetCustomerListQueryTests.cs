using System;
using Xunit;
using Northwind.Application.Customers.Queries.GetCustomerList;
using Northwind.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Northwind.Domain;

namespace Northwind.Application.Tests
{
    public class GetCustomerListQueryTests
    {
        [Fact]
        public async Task ShouldReturnAllCustomers()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseInMemoryDatabase()
                    .Options;

            var context = new NorthwindContext(options);

            context.Customers.AddRange(new [] {
                new Customer { CustomerId = "ADAM", CompanyName = "Adam Cogan" },
                new Customer { CustomerId = "JASON", CompanyName = "Jason Taylor" },
                new Customer { CustomerId = "BREND", CompanyName = "Brendan Richards" },
            });

            context.SaveChanges();

            var query = new GetCustomerListQuery(context);

            var result = await query.Execute();

            Assert.Equal(3, result.Count());
        }
    }
}
