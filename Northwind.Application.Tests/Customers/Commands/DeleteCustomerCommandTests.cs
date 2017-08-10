using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Xunit;

namespace Northwind.Application.Tests.Customers.Commands
{
    public class DeleteCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteCustomerFromDatabase()
        {
            var command = new DeleteCustomerCommand(_context);

            await command.Execute("JASON");

            var entity = await _context.Customers.FindAsync("JASON");

            Assert.Null(entity);
        }
    }
}