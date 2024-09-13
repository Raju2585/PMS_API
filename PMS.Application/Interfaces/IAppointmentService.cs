using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;

namespace PMS.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> ScheduleAppointment(AppointmentDto appointmentDto);
        Task<AppointmentDto> GetAppointment(int appointmentId);
        Task<AppointmentDto> UpdateAppointment(int appointmentId, RequestAppointmentDto appointment);
        Task<List<AppointmentDto>> GetAppointmentsByPatientId(int patientId);
        Task<List<AppointmentDto>> GetAppointmentsByDoctorId(int doctorId);
        Task<Appointment> UpdateAppointmentStatus(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByHospital(string hospital);

    }
}
