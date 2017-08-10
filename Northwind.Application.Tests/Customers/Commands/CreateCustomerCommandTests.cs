using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Xunit;

namespace Northwind.Application.Tests.Customers.Commands
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldInsertCustomerIntoDatabase()
        {
            var command = new CreateCustomerCommand(_context);

            var model = new CreateCustomerModel
            {
                CustomerId = "FLYNN",
                CompanyName = "Flynn's Arcade",
                ContactName = "Kevin"
            };

            await command.Execute(model);

            var entity = await _context.Customers.FindAsync("FLYNN");

            Assert.NotNull(entity);
        }
    }
}