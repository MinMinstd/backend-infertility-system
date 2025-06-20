using AutoMapper;
using infertility_system.Dtos.Service;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceController(IServiceRepository serviceRepository, IMapper mapper)
        {
            _mapper = mapper;
            _serviceRepository = serviceRepository;
        }
        [HttpGet("GetListServices")]
        public async Task<IActionResult> GetAllServices([FromQuery] QueryService query)
        {
            var services = await _serviceRepository.GetListServicesAsync(query);
            var servicesDto = _mapper.Map<List<ServiceToDtoForList>>(services);
            return Ok(servicesDto);
        }

        [HttpGet("GetAllServicesToBooking")]
        public async Task<IActionResult> GetAllServicesToBooking()
        {
            var services = await _serviceRepository.GetAllServiceToBooking();
            var servicesDto = _mapper.Map<List<ServiceToBookingDto>>(services);
            return Ok(servicesDto);
        }
    }

}
