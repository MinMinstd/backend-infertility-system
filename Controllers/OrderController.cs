using AutoMapper;
using infertility_system.Dtos.Order;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetOrderCurrent")]
        public async Task<IActionResult> GetOrderCurrent()
        {
            var userId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var customer = await this.customerRepository.GetCustomersAsync(userId);
            var order = await this.orderRepository.GetOrderCurrent(customer.CustomerId);
            if (order == null)
            {
                return NotFound("No current order found for the customer.");
            }
            var orderDto = this.mapper.Map<OrderToPaymentDto>(order);
            return Ok(orderDto);
        }
    }
}
