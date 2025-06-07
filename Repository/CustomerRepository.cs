using infertility_system.Data;
using infertility_system.Dtos.User;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            if (user.Password != dto.CurrentPassword)
            {
                return Task.FromResult(false);
            }
            if(dto.NewPassword != dto.ConfirmPassword)
            {
                return Task.FromResult(false);
            }
            user.Password = dto.NewPassword;
            _context.Users.Update(user);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _context.Customers.AnyAsync(x => x.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(int userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<ICollection<Embryo>> GetEmbryos(int userId)
        {
            var embryos = await _context.Embryos.Where(x => x.Customer.UserId == userId).ToListAsync();
            return embryos;
        }

        public async Task<ICollection<MedicalRecordDetail>> GetMedicalRecords(int userId)
        {
            var records = await _context.MedicalRecordDetails
                .Include(tr => tr.TreatmentRoadmap)
                    .ThenInclude(s => s.Service)
                .Include(mr => mr.MedicalRecord)
                    .ThenInclude(d => d.Doctor)
                .Where(mr => mr.MedicalRecord.CustomerId == userId).ToListAsync();

            return records;
        }
    }
}
