namespace infertility_system.Data
{
    using infertility_system.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.BlogPost> BlogPosts { get; set; }

        public DbSet<Models.Booking> Bookings { get; set; }

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

        public DbSet<Models.ServiceDB> Services { get; set; }

        public DbSet<Models.TreatmentResult> TreatmentResults { get; set; }

        public DbSet<Models.TreatmentRoadmap> TreatmentRoadmaps { get; set; }

        public DbSet<Models.TypeTest> TypeTests { get; set; }

        public DbSet<Models.User> Users { get; set; }

        public DbSet<Models.Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BlogPost
            modelBuilder.Entity<BlogPost>()
                .HasOne(bp => bp.Manager)
                .WithMany(m => m.BlogPosts)
                .HasForeignKey(bp => bp.ManagerId);

            modelBuilder.Entity<BlogPost>()
                .HasOne(bp => bp.Customer)
                .WithMany(c => c.BlogPosts)
                .HasForeignKey(bp => bp.CustomerId);

            // Manager
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Services)
                .WithOne(s => s.Manager)
                .HasForeignKey(s => s.ManagerId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Feedbacks)
                .WithOne(f => f.Manager)
                .HasForeignKey(f => f.ManagerId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Orders)
                .WithOne(o => o.Manager)
                .HasForeignKey(o => o.ManagerId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.DoctorSchedules)
                .WithOne(ds => ds.Manager)
                .HasForeignKey(ds => ds.ManagerId);

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.BlogPosts)
                .WithOne(bp => bp.Manager)
                .HasForeignKey(bp => bp.ManagerId);

            // Embryo
            modelBuilder.Entity<Embryo>()
                .HasOne(e => e.Customer)
                .WithMany(c => c.Embryos)
                .HasForeignKey(e => e.CustomerId);

            // Customer
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Feedbacks)
                .WithOne(f => f.Customer)
                .HasForeignKey(f => f.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.BlogPosts)
                .WithOne(bp => bp.Customer)
                .HasForeignKey(bp => bp.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.MedicalRecord)
                .WithOne(mr => mr.Customer)
                .HasForeignKey(mr => mr.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            // DoctorDegree
            modelBuilder.Entity<DoctorDegree>()
                .HasOne(dd => dd.Doctor)
                .WithMany(d => d.DoctorDegrees)
                .HasForeignKey(dd => dd.DoctorId);

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.DoctorDegrees)
                .WithOne(dd => dd.Doctor)
                .HasForeignKey(dd => dd.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.DoctorSchedules)
                .WithOne(ds => ds.Doctor)
                .HasForeignKey(ds => ds.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.MedicalRecords)
                .WithOne(mr => mr.Doctor)
                .HasForeignKey(mr => mr.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.ServiceDB)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.ServiceDBId);

            // DoctorSchedule
            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Manager)
                .WithMany(m => m.DoctorSchedules)
                .HasForeignKey(ds => ds.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSchedule>()
                .HasMany(ds => ds.Bookings)
                .WithOne(b => b.DoctorSchedule)
                .HasForeignKey(b => b.DoctorScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Manager)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Booking)
                .WithOne(b => b.Order)
                .HasForeignKey<Order>(o => o.BookingId);

            // Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.DoctorSchedule)
                .WithMany(ds => ds.Bookings)
                .HasForeignKey(b => b.DoctorScheduleId);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.ConsulationResult)
                .WithOne(cr => cr.Booking)
                .HasForeignKey(cr => cr.BookingId);

            //// ConsulationRegistration

            // modelBuilder.Entity<ConsulationRegistration>()
            //    .HasMany(cr => cr.ConsulationResult)
            //    .WithOne(cr => cr.ConsulationRegistration)
            //    .HasForeignKey(cr => cr.ConsulationRegistrationId);

            // OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Service)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.ServiceId);

            // Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CustomerId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Manager)
                .WithMany(m => m.Feedbacks)
                .HasForeignKey(f => f.ManagerId);

            // Service
            modelBuilder.Entity<ServiceDB>()
                .HasOne(s => s.Manager)
                .WithMany(m => m.Services)
                .HasForeignKey(s => s.ManagerId);

            modelBuilder.Entity<ServiceDB>()
                .HasMany(s => s.OrderDetails)
                .WithOne(od => od.Service)
                .HasForeignKey(od => od.ServiceId);

            modelBuilder.Entity<ServiceDB>()
                .HasMany(s => s.TreatmentRoadmaps)
                .WithOne(tr => tr.Service)
                .HasForeignKey(tr => tr.ServiceId);

            modelBuilder.Entity<ServiceDB>()
                .HasMany(s => s.Doctors)
                .WithOne(d => d.ServiceDB)
                .HasForeignKey(d => d.ServiceDBId);

            // MedicalRecord
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Customer)
                .WithMany(c => c.MedicalRecord)
                .HasForeignKey(mr => mr.CustomerId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Doctor)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(mr => mr.DoctorId);

            modelBuilder.Entity<MedicalRecord>()
                .HasMany(mr => mr.MedicalRecordDetails)
                .WithOne(mrd => mrd.MedicalRecord)
                .HasForeignKey(mrd => mrd.MedicalRecordId);

            // ConsulationResult
            // modelBuilder.Entity<ConsulationResult>()
            //    .HasOne(cr => cr.ConsulationRegistration)
            //    .WithMany(cr => cr.ConsulationResult)
            //    .HasForeignKey(cr => cr.ConsulationRegistrationId);
            modelBuilder.Entity<ConsulationResult>()
                .HasMany(cr => cr.MedicalRecordDetails)
                .WithOne(mrd => mrd.ConsulationResult)
                .HasForeignKey(mrd => mrd.ConsulationResultId);

            modelBuilder.Entity<ConsulationResult>()
                .HasMany(cr => cr.TypeTests)
                .WithOne(p => p.ConsulationResult)
                .HasForeignKey(p => p.ConsulationResultId);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            // TreatmentRoadmap
            modelBuilder.Entity<TreatmentRoadmap>()
                .HasKey(tr => tr.TreatmentRoadmapId);

            modelBuilder.Entity<TreatmentRoadmap>()
                .Property(tr => tr.TreatmentRoadmapId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TreatmentRoadmap>()
                .HasMany(tr => tr.TreatmentResults)
                .WithOne(tr => tr.TreatmentRoadmap)
                .HasForeignKey(tr => tr.TreatmentRoadmapId);

            modelBuilder.Entity<TreatmentRoadmap>()
                .HasOne(tr => tr.Service)
                .WithMany(s => s.TreatmentRoadmaps)
                .HasForeignKey(tr => tr.ServiceId);

            modelBuilder.Entity<TreatmentRoadmap>()
                .HasOne(tr => tr.Payment)
                .WithOne(p => p.TreatmentRoadmap)
                .HasForeignKey<Payment>(p => p.TreatmentRoadmapId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TreatmentRoadmap>()
                .HasMany(tr => tr.MedicalRecordDetails)
                .WithOne(p => p.TreatmentRoadmap)
                .HasForeignKey(p => p.TreatmentRoadmapId);

            // MedicalRecordDetail
            modelBuilder.Entity<MedicalRecordDetail>()
                .HasOne(mrd => mrd.MedicalRecord)
                .WithMany(mr => mr.MedicalRecordDetails)
                .HasForeignKey(mrd => mrd.MedicalRecordId);

            modelBuilder.Entity<MedicalRecordDetail>()
                .HasOne(mrd => mrd.ConsulationResult)
                .WithMany(cr => cr.MedicalRecordDetails)
                .HasForeignKey(mrd => mrd.ConsulationResultId);

            modelBuilder.Entity<MedicalRecordDetail>()
                .HasOne(mrd => mrd.TreatmentResult)
                .WithMany(tr => tr.MedicalRecordDetails)
                .HasForeignKey(mrd => mrd.TreatmentResultId)
                .OnDelete(DeleteBehavior.NoAction);

            // PrescriptionDetail
            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(pd => pd.Prescription)
                .WithMany(p => p.PrescriptionDetails)
                .HasForeignKey(pd => pd.PrescriptionId);

            // Prescription
            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionDetails)
                .WithOne(pd => pd.Prescription)
                .HasForeignKey(pd => pd.PrescriptionId);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.TreatmentResult)
                .WithMany(mrd => mrd.Prescriptions)
                .HasForeignKey(p => p.TreatmentResultId);

            // TreatmentResult
            modelBuilder.Entity<TreatmentResult>()
                .HasMany(tr => tr.MedicalRecordDetails)
                .WithOne(mrd => mrd.TreatmentResult)
                .HasForeignKey(mrd => mrd.TreatmentResultId);

            modelBuilder.Entity<TreatmentResult>()
                .HasMany(tr => tr.Prescriptions)
                .WithOne(p => p.TreatmentResult)
                .HasForeignKey(p => p.TreatmentResultId);

            modelBuilder.Entity<TreatmentResult>()
                .HasOne(tr => tr.TreatmentRoadmap)
                .WithMany(trm => trm.TreatmentResults)
                .HasForeignKey(tr => tr.TreatmentRoadmapId);

            modelBuilder.Entity<TreatmentResult>()
                .HasMany(tr => tr.TypeTest)
                .WithOne(tt => tt.TreatmentResult)
                .HasForeignKey(tt => tt.TreatmentResultId);

            // TypeTest
            modelBuilder.Entity<TypeTest>()
                .HasOne(tt => tt.ConsulationResult)
                .WithMany(cr => cr.TypeTests)
                .HasForeignKey(tt => tt.ConsulationResultId);

            modelBuilder.Entity<TypeTest>()
                .HasOne(tt => tt.TreatmentResult)
                .WithMany(tr => tr.TypeTest)
                .HasForeignKey(tt => tt.TreatmentResultId);

            // User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId)
                .HasConstraintName(null);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId)
                .HasConstraintName(null)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
