using AutoMapper;
using infertility_system.Dtos.ConsulationResult;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class ConsulationResultController : ControllerBase
    {
        private readonly IConsulationResultRepository _consulationResultRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        public ConsulationResultController(IConsulationResultRepository consulationResult, IBookingRepository bookingRepository, IMapper mapper)
        {
            _consulationResultRepository = consulationResult;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsulationResult([FromBody] ConsulationResultRequest request)
        {
            var consulationResult = _mapper.Map<ConsulationResult>(request);

            await _bookingRepository.UpdateBookingStatusAsync(request.BookingId.Value);
            await _consulationResultRepository.CreateConsulationResultAsync(consulationResult);

            return Ok("Consultation result processed successfully.");
        }
    }
}
