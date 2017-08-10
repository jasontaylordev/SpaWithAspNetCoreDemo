using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Customers.Queries.GetCustomerList;
using Northwind.Domain;
using Northwind.Persistance;
using Northwind.Presentation.Filters;

namespace Northwind.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IGetCustomerDetailQuery _getCustomerDetailQuery;
        private readonly IGetCustomerListQuery _getCustomerListQuery;
        private readonly ICreateCustomerCommand _createCustomerCommand;
        private readonly IUpdateCustomerCommand _updateCustomerCommand;
        private readonly IDeleteCustomerCommand _deleteCustomerCommand;

        public CustomersController(
            IGetCustomerListQuery getCustomerListQuery,
            IGetCustomerDetailQuery getCustomerDetailQuery,            
            ICreateCustomerCommand createCustomerCommand,
            IUpdateCustomerCommand updateCustomerCommand,
            IDeleteCustomerCommand deleteCustomerCommand)
        {
            _getCustomerListQuery = getCustomerListQuery;
            _getCustomerDetailQuery = getCustomerDetailQuery;            
            _createCustomerCommand = createCustomerCommand;
            _updateCustomerCommand = updateCustomerCommand;
            _deleteCustomerCommand = deleteCustomerCommand;

        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerListModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers()
        {
            return  Ok(await _getCustomerListQuery.Execute());
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [ValidateCustomerExists]
        [ProducesResponseType(typeof(CustomerDetailModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomer([FromRoute] string id)
        {
            return Ok(await _getCustomerDetailQuery.Execute(id));
        }

        // POST: api/Customers
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostCustomer([FromBody] CreateCustomerModel customer)
        {
            await _createCustomerCommand.Execute(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [ValidateModel, ValidateCustomerExists]
        [ProducesResponseType(typeof(CustomerDetailModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutCustomer([FromRoute] string id, [FromBody] UpdateCustomerModel customer)
        {
            await _updateCustomerCommand.Execute(customer);

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ValidateCustomerExists]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
        {
            await _deleteCustomerCommand.Execute(id);

            return NoContent();
        }
    }
}