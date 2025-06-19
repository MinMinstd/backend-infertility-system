using AutoMapper;
using infertility_system.Data;
using infertility_system.Interfaces;
using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Repository
{
    public class MedicalRecordDetailRepository : IMedicalRecordDetailRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public MedicalRecordDetailRepository(AppDbContext context, IMapper mapper, ICustomerRepository customerRepository)
        {
            _context = context;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MedicalRecord>> GetMedicalRecordAsync(int userId)
        {
            var isValid = await _customerRepository.CheckCustomerExistsAsync(userId);
            if (!isValid) return null;

            var customer = await _customerRepository.GetCustomersAsync(userId);

            var medicalRecords = await _context.MedicalRecords
                        .Where(mr => mr.CustomerId == customer.CustomerId)
                        .ToListAsync();
            return medicalRecords;
        }

        public async Task<ICollection<MedicalRecord>> GetMedicalRecordWithDetailsAsync(int userId)
        {
            var isValid = await _customerRepository.CheckCustomerExistsAsync(userId);
            if (!isValid) return null;

            var customer = await _customerRepository.GetCustomersAsync(userId);

            var records = await _context.MedicalRecords
                .Where(x => x.CustomerId == customer.CustomerId)
                .Include(x => x.MedicalRecordDetails)
                .ToListAsync();

            return records;
        }

    }
}
