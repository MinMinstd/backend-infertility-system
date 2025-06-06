using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices([FromQuery] QueryService query)
        {
            var services = await _serviceRepository.GetAllServicesAsync(query);
            var servicesDto = services.Select(s => s.ToDtoForList()).ToList();
            return Ok(servicesDto);
        }
    }
}
