using AutoMapper;
using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class DoctorSchedulesRepository : IDoctorSchedulesRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorSchedulesRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DoctorSchedule>> GetDoctorScheduleAsync(int doctorId)
        {
            return await _context.DoctorSchedules.Where(x => x.DoctorId == doctorId).ToListAsync();
        }
    }
}
