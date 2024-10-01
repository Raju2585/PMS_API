using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using PMS.Domain.Entities.Response;

namespace PMS.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentRes> ScheduleAppointment(AppointmentDto appointmentDto);
        Task<AppointmentDto> GetAppointment(int appointmentId);
        Task<AppointmentDto> UpdateAppointment(int appointmentId, AppointmentDto appointment);
        Task<List<AppointmentDto>> GetAppointmentsByPatientId(string patientId);
        Task<List<AppointmentDto>> GetAppointmentsByDoctorId(int doctorId);
        Task<Appointment> UpdateAppointmentStatus(Appointment appointment);
        Task<List<AppointmentDto>> GetAppointmentsByHospital(string hospital);
        Task<Appointment> UpdateAppointmentStatus(int appointmentId, int statusId);
    }
}
