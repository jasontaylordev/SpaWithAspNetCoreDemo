using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Xunit;

namespace Northwind.Application.Tests.Customers.Commands
{
    public class UpdateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldUpdateCustomerInDatabase()
        {
            var command = new UpdateCustomerCommand(_context);

            var model = new UpdateCustomerModel {
                CustomerId = "JASON",
                CompanyName = "Jason Inc",
                ContactName = "Jason Taylor"
            };

            await command.Execute(model);

            var entity = await _context.Customers.FindAsync("JASON");

            Assert.Equal("Jason Inc", entity.CompanyName);
        }
    }
}