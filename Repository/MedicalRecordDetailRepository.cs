﻿namespace infertility_system.Repository
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

        public async Task<ICollection<MedicalRecordDetail>> GetMedicalRecordDetailWithTreatmentResultAndTypetestAsync(int medicalRecordId)
        {
            var medicalRecord = await this.context.MedicalRecords.FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId);

            var medicalRecordDetails = await this.context.MedicalRecordDetails
                            .Where(mrd => mrd.MedicalRecordId == medicalRecord.MedicalRecordId)
                            .Include(mrd => mrd.TreatmentResult)
                            .ThenInclude(tr => tr.TypeTest)
                            .ToListAsync();
            return medicalRecordDetails;
        }
    }
}
