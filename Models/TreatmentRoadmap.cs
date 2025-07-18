﻿namespace infertility_system.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TreatmentRoadmap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentRoadmapId { get; set; }

        public DateOnly Date { get; set; }

        public string? Stage { get; set; }

        public string? Description { get; set; }

        public int DurationDay { get; set; }

        public decimal Price { get; set; }

        // TreatmentRoadmap N-1 Service
        public int ServiceId { get; set; }

        public ServiceDB? Service { get; set; }

        // TreatmentRoadmap 1-N MedicalRecordDetail
        public List<MedicalRecordDetail>? MedicalRecordDetails { get; set; }

        // TreatmentRoadmap 1-N Payment
        public List<Payment>? Payment { get; set; }

        // TreatmentRoadmap 1-N TreatmentResult
        public List<TreatmentResult>? TreatmentResults { get; set; }
    }
}
