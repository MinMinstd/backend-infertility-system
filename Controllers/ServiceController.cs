namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.Service;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IMapper mapper;

        public ServiceController(IServiceRepository serviceRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
        }

        [HttpGet("GetListServices")]
        public async Task<IActionResult> GetAllServices([FromQuery] QueryService query)
        {
            var services = await this.serviceRepository.GetListServicesAsync(query);
            var servicesDto = this.mapper.Map<List<ServiceToDtoForList>>(services);
            return this.Ok(servicesDto);
        }

        [HttpGet("GetAllServicesToBooking")]
        public async Task<IActionResult> GetAllServicesToBooking()
        {
            var services = await this.serviceRepository.GetAllServiceToBooking();
            var servicesDto = this.mapper.Map<List<ServiceToBookingDto>>(services);
            return this.Ok(servicesDto);
        }

        [HttpGet("GetServicesForManagement")]
        public async Task<IActionResult> GetServicesForManagement()
        {
            var services = await this.serviceRepository.GetServicesForManagement();
            var servicesDto = this.mapper.Map<List<ServiceToDtoForList>>(services);
            return this.Ok(servicesDto);
        }
    }
}
