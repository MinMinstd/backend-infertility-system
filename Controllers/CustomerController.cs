using infertility_system.Dtos.Customer;
using infertility_system.Interfaces;
using infertility_system.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ManagerDoctorOnly")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return Ok(customers.Select(c => c.ToCustomerDto()));
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToCustomerDto());
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestDto customerDto)
        {
            var customer = customerDto.ToCustomerFromCreateDto();
            var newCustomer = await _customerRepository.CreateCustomerAsync(customer);
            return (CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.CustomerId }, newCustomer.ToCustomerDto()));
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequestDto customerDto)
        {
            var customer = customerDto.ToCustomerFromUpdateDto();
            var updatedCustomer = await _customerRepository.UpdateCustomerAsync(id, customer);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer.ToCustomerDto());
        }


        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerRepository.DeleteCustomerAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
