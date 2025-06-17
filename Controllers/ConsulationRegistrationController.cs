using AutoMapper;
using infertility_system.Dtos.ConsulationRegistration;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class ConsulationRegistrationController : ControllerBase
    {
        private readonly IConsulationRegistrationRepository _repository;
        private readonly IMapper _mapper;
        public ConsulationRegistrationController(IConsulationRegistrationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registrations = await _repository.GetAllRegistrationsAsync();
            var registrationsDto = _mapper.Map<List<ConsulationRegistrationRespond>>(registrations);
            return Ok(registrationsDto);
        }
    }
}
