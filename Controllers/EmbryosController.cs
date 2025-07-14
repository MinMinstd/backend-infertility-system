using AutoMapper;
using infertility_system.Data;
using infertility_system.Dtos.Embryo;
using infertility_system.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbryosController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IEmbryoRepository embryoRepository;
        private readonly IMapper mapper;

        public EmbryosController(AppDbContext context, IEmbryoRepository embryoRepository, IMapper mapper)
        {
            this.context = context;
            this.embryoRepository = embryoRepository;
            this.mapper = mapper;
        }

        [HttpGet("embryos/{customerId}/{bookingId}")]
        public async Task<IActionResult> GetListEmbryoInDoctor(int customerId, int bookingId)
        {
            var embryos = await this.embryoRepository.GetEmbryosInDoctorAsync(bookingId, customerId);
            var result = this.mapper.Map<List<EmbryoDto>>(embryos);
            return Ok(result);
        }

        [HttpPost("embryo/{bookingId}")]
        public async Task<IActionResult> CreateEmbryo([FromBody] CreateEmbryoDto dto, int bookingId)
        {
            var result = await this.embryoRepository.CreateEmbryoAsync(dto, bookingId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }

        [HttpPut("embryo/{embryoId}")]
        public async Task<IActionResult> UpdateEmbryo([FromBody] UpdateEmbryoDto dto, int embryoId)
        {
            var result = await this.embryoRepository.UpdateEmbryoAsync(dto, embryoId);
            return result ? this.Ok("Successfully") : this.BadRequest("Fail");
        }
    }
}
