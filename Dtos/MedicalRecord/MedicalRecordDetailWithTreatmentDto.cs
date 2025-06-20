﻿namespace infertility_system.Dtos.MedicalRecord
{
    public class MedicalRecordDetailWithTreatmentDto
    {
        public DateOnly Date { get; set; }
        public string? TestResult { get; set; }
        public string? Note { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Stage { get; set; }
    }
}
