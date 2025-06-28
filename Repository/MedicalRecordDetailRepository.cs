namespace infertility_system.Repository
{
    using AutoMapper;
    using infertility_system.Data;
    using infertility_system.Interfaces;
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class MedicalRecordDetailRepository : IMedicalRecordDetailRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepository;

        public MedicalRecordDetailRepository(AppDbContext context, IMapper mapper, ICustomerRepository customerRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.customerRepository = customerRepository;
        }

        public Task<MedicalRecordDetail> CreateMedicalRecordDetailAsync(MedicalRecordDetail medicalRecordDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MedicalRecord>> GetMedicalRecordAsync(int userId)
        {
            var isValid = await this.customerRepository.CheckCustomerExistsAsync(userId);
            if (!isValid)
            {
                return null;
            }

            var customer = await this.customerRepository.GetCustomersAsync(userId);

            var medicalRecords = await this.context.MedicalRecords
                        .Where(mr => mr.CustomerId == customer.CustomerId)
                        .ToListAsync();
            return medicalRecords;
        }

        public async Task<ICollection<MedicalRecord>> GetMedicalRecordWithDetailsAsync(int userId)
        {
            var isValid = await this.customerRepository.CheckCustomerExistsAsync(userId);
            if (!isValid)
            {
                return null;
            }

            var customer = await this.customerRepository.GetCustomersAsync(userId);

            var records = await this.context.MedicalRecords
                .Where(x => x.CustomerId == customer.CustomerId)
                .Include(m => m.MedicalRecordDetails)
                .ThenInclude(mrd => mrd.TreatmentRoadmap)
                .ToListAsync();

            return records;
        }

        public async Task<ICollection<MedicalRecordDetail>> GetMedicalRecordDetailTypetestBaseTreatmentCompleteAsync(int userId)
        {
            var isValid = await this.customerRepository.CheckCustomerExistsAsync(userId);
            if (!isValid)
            {
                return null;
            }

            var customer = await this.customerRepository.GetCustomersAsync(userId);

            var medicalRecord = await this.context.MedicalRecords.
                                FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
            if (medicalRecord == null)
            {
                return null;
            }

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(x => x.MedicalRecordId == medicalRecord.MedicalRecordId && x.TreatmentResultId != null)
                            .Include(x => x.TreatmentResult)
                            .ThenInclude(tr => tr.TypeTest)
                            .ToListAsync();

            return medicalRecordDetails;
        }
    }
}
