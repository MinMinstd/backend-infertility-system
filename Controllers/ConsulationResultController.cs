namespace infertility_system.Controllers
{
    using AutoMapper;
    using infertility_system.Dtos.ConsulationResult;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class ConsulationResultController : ControllerBase
    {
        private readonly IConsulationResultRepository consulationResultRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IMapper mapper;

        public ConsulationResultController(IConsulationResultRepository consulationResult, IBookingRepository bookingRepository, IMapper mapper)
        {
            this.consulationResultRepository = consulationResult;
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsulationResult([FromBody] ConsulationResultRequest request)
        {
            var consulationResult = this.mapper.Map<ConsulationResult>(request);

            await this.bookingRepository.UpdateBookingStatusAsync(request.BookingId.Value);
            await this.consulationResultRepository.CreateConsulationResultAsync(consulationResult);

            return this.Ok("Consultation result processed successfully.");
        }
    }
}
