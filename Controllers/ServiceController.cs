using AutoMapper;
using infertility_system.Dtos.Service;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Models;
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

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            var servicesDto = _mapper.Map<List<ServiceToDtoForList>>(services);
            return Ok(servicesDto);
        }
    }

}
