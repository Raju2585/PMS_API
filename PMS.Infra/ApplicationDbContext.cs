using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Domain;
using PMS.Domain.Entities;
namespace PMS.Infra
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Doctor_Slots> Doctor_Slots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            // Configure one-to-one relationship between Patient and Device
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Patient)
                .WithOne(p => p.Device)
                .HasForeignKey<Device>(d => d.Id)
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
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Patient)
                .WithMany(p => p.Consultations)
                .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Consultations)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(m => m.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Receptionist>()
               .HasOne(r => r.Hospital)
               .WithOne(h => h.Receptionist)
               .HasForeignKey<Receptionist>(r => r.HospitalId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>()
                .HasOne(s => s.Patient)
                .WithMany(p => p.Notification)
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor_Slots>()
                .HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
