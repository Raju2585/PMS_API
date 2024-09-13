using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public interface IApplicationDbContext
    {
        DbSet<Patient> Patients { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Consultation> Consultations { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<MedicalHistory> MedicalHistories { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<VitalSign> VitalSigns { get; set; }
        DbSet<Receptionist> Receptionists { get; set; }
        DbSet<Hospital> Hospitals { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
