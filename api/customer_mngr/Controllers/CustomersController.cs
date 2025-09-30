using customer_mngr.Data;
using customer_mngr.Models;
using customer_mngr.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace customer_mngr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDBContext dBContext;

        public CustomersController(CustomerDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers() { 
            // fetch the table
            var customers = await dBContext.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCustomer(AddCustomerRequestDTO req)
        {
            // Add new user (post method)
            var customerModel = new Customer
            {
                id = new Guid(),
                name = req.name,
                address = req.address,
                email = req.email,
                phone = req.phone
            };
            dBContext.Customers.Add(customerModel);
            await dBContext.SaveChangesAsync();
            return Ok(customerModel);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var _customer = await dBContext.Customers.FindAsync(id);
            if (_customer != null) {
                dBContext.Customers.Remove(_customer);
                await dBContext.SaveChangesAsync();
            }
            return Ok();
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditCustomer(Guid id, [FromBody] Customer updatedCustomer)
        {
            var customer = await dBContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            // Update fields
            if(updatedCustomer.name is not null)
                customer.name = updatedCustomer.name;
            if (updatedCustomer.email is not null)
                customer.email = updatedCustomer.email;
            if (updatedCustomer.phone is not null)
                customer.phone = updatedCustomer.phone;
            if (updatedCustomer.address is not null)
                customer.address = updatedCustomer.address;

            await dBContext.SaveChangesAsync();

            return Ok(customer);
        }
    }
}
