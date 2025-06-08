using AutoMapper;
using infertility_system.Dtos.Doctor;
using infertility_system.Helpers;
using infertility_system.Interfaces;
using infertility_system.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace infertility_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository doctorRepository,IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] QueryDoctor query)
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync(query);
            var doctorDto = _mapper.Map<List<DoctorForListDto>>(doctors);
            return Ok(doctorDto);
        }

        [HttpGet("GetDoctorById/{doctorId}")]
        public async Task<IActionResult> GetDoctorById(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {doctorId} not found.");
            }
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);


        }
    }
}
