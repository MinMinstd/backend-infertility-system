using infertility_system.Models;
using Microsoft.EntityFrameworkCore;

namespace infertility_system.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.BlogPost> BlogPosts { get; set; }
        public DbSet<Models.Booking> Bookings { get; set; }
        public DbSet<Models.ConsulationRegistration> ConsulationRegistrations { get; set; }
        public DbSet<Models.ConsulationResult> ConsulationResults { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.DoctorDegree> DoctorDegrees { get; set; }
        public DbSet<Models.DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Models.Embryo> Embryos { get; set; }
        public DbSet<Models.Feedback> Feedbacks { get; set; }
        public DbSet<Models.Manager> Managers { get; set; }
        public DbSet<Models.MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Models.MedicalRecordDetail> MedicalRecordDetails { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.OrderDetail> OrderDetails { get; set; }
        public DbSet<Models.Payment> Payments { get; set; }
        public DbSet<Models.Prescription> Prescriptions { get; set; }
        public DbSet<Models.PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<Models.Service> Services { get; set; }
        public DbSet<Models.TreatmentResult> TreatmentResults { get; set; }
        public DbSet<Models.TreatmentRoadmap> TreatmentRoadmaps { get; set; }
        public DbSet<Models.TypeTest> TypeTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
