using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class ConsulationRegistrationRepository : IConsulationRegistrationRepository
    {
        private readonly AppDbContext _context;
        public ConsulationRegistrationRepository(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<List<ConsulationRegistration>> GetAllRegistrationsAsync()
        //{
        //    return await _context.ConsulationRegistrations.ToListAsync();
        //}
    }
}
