using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;

namespace PMS.Infra
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IApplicationDbContext _applicationContext;
        public AppointmentRepository(IApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Appointment> ScheduleAppointment(Appointment appointment)
        {
            _applicationContext.Appointments.Add(appointment);
            await _applicationContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> GetAppointment(int appointmentId)
        {
            return await _applicationContext.Appointments.FindAsync(appointmentId);
        }

        public async Task<Appointment> UpdateAppointment(int appointmentId, Appointment updatedAppointment)
        {
            var existingAppointment = await GetAppointment(appointmentId);

            if (existingAppointment == null)
                return null;

            existingAppointment.PatientId = updatedAppointment.PatientId;
            existingAppointment.DoctorId = updatedAppointment.DoctorId;
            existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;
            existingAppointment.StatusId = updatedAppointment.StatusId;
            existingAppointment.Reason = updatedAppointment.Reason;
            existingAppointment.CreatedAt = updatedAppointment.CreatedAt;

            _applicationContext.Appointments.Update(existingAppointment);
            await _applicationContext.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientId(int patientId)
        {
            return await _applicationContext.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            return await _applicationContext.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> UpdateAppointmentStatus(Appointment existingAppointment)
        {
            if (existingAppointment == null)
                return null;

            _applicationContext.Appointments.Update(existingAppointment);
            await _applicationContext.SaveChangesAsync();

            return existingAppointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByHospital(string hospitalName)
        {
            if (string.IsNullOrWhiteSpace(hospitalName))
                throw new ArgumentException("Hospital name cannot be null or empty", nameof(hospitalName));

            return await _applicationContext.Appointments
                .Where(a => a.HospitalName == hospitalName)
                
                .ToListAsync();
        }

    }
}
