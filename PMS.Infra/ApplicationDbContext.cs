using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using PMS.Domain;
using PMS.Domain.Entities;
namespace PMS.Infra
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-one relationship between Patient and Device
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Patient)
                .WithOne(p => p.Device)
                .HasForeignKey<Device>(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-one relationship between Device and VitalSign
            modelBuilder.Entity<VitalSign>()
           .HasOne(v => v.Device)  
           .WithOne()  
           .HasForeignKey<VitalSign>(v => v.DeviceId)  
           .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-many relationship between Doctor and Hospital
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-one relationships for other entities
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Patient)
                .WithMany(p => p.Consultations)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Consultations)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);


        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
