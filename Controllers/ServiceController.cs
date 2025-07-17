namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.Service;
    using infertility_system.Helpers;
    using infertility_system.Interfaces;
    using infertility_system.Models;
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

        [HttpGet("for-management")]
        public async Task<IActionResult> GetServicesForManagement()
        {
            var services = await this.serviceRepository.GetServicesForManagement();
            var servicesDto = this.mapper.Map<List<ServiceToDtoForList>>(services);
            return this.Ok(servicesDto);
        }

        [HttpGet("{serviceDBId}")]
        public async Task<IActionResult> GetServiceById(int serviceDBId)
        {
            var service = await this.serviceRepository.GetServiceByIdAsync(serviceDBId);
            if (service == null)
            {
                return NotFound("Service not found.");
            }

            var serviceDto = this.mapper.Map<RequestServiceDto>(service);
            return this.Ok(serviceDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] RequestServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                return BadRequest("Service data is null.");
            }

            if (string.IsNullOrEmpty(serviceDto.Name) || string.IsNullOrEmpty(serviceDto.Description))
            {
                return BadRequest("Service name and description cannot be null.");
            }

            var service = this.mapper.Map<ServiceDB>(serviceDto);
            service.ManagerId = 1;
            await this.serviceRepository.AddServiceAsync(service);
            return this.Ok("Service added successfully.");
        }

        [HttpPut("{serviceDBId}")]
        public async Task<IActionResult> UpdateService(int serviceDBId, [FromBody] RequestServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                return BadRequest("Service data is null.");
            }

            if (string.IsNullOrEmpty(serviceDto.Name) || string.IsNullOrEmpty(serviceDto.Description))
            {
                return BadRequest("Service name and description cannot be null.");
            }

            var service = this.mapper.Map<ServiceDB>(serviceDto);
            var result = await this.serviceRepository.UpdateServiceAsync(serviceDBId, service);
            if (!result)
            {
                return NotFound("Service not found.");
            }

            return this.Ok("Service updated successfully.");
        }
    }
}
