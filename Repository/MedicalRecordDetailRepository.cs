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

        public MedicalRecordDetailRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MedicalRecordDetail>> GetMedicalRecordsDetailAsync(int userId)
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
