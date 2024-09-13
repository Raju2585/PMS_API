using AutoMapper;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;

namespace PMS.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository appointmentRepository,IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> ScheduleAppointment(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            if (appointmentDto.AppointmentDate < DateTime.UtcNow)
                throw new ArgumentException("Appointment date cannot be in the past.");

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            var scheduledAppointment = await _appointmentRepository.ScheduleAppointment(appointment);
            var scheduledAppointmentDto = _mapper.Map<AppointmentDto>(scheduledAppointment);

            return scheduledAppointmentDto;
        }

        public async Task<AppointmentDto> GetAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.", nameof(appointmentId));
            }

            var appointment = await _appointmentRepository.GetAppointment(appointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            var appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            return appointmentDto;
        }


        public async Task<AppointmentDto> UpdateAppointment(int appointmentId, RequestAppointmentDto updatedAppointmentDto)
        {
            if (updatedAppointmentDto == null)
                throw new ArgumentNullException(nameof(updatedAppointmentDto));

            var existingAppointment = await _appointmentRepository.GetAppointment(appointmentId);

            if (existingAppointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            var updatedAppointment = _mapper.Map<Appointment>(updatedAppointmentDto);

            existingAppointment = _mapper.Map(updatedAppointment, existingAppointment);

            var result = await _appointmentRepository.UpdateAppointment(appointmentId,existingAppointment);

            var updatedAppointmentDtoResult = _mapper.Map<AppointmentDto>(result);

            return updatedAppointmentDtoResult;
        }


        public async Task<List<AppointmentDto>> GetAppointmentsByPatientId(int patientId)
        {
            if (patientId <= 0)
            {
                throw new ArgumentException("Invalid patient ID.", nameof(patientId));
            }

            var appointments = await _appointmentRepository.GetAppointmentsByPatientId(patientId);

            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return appointmentDtos;
        }

        public async Task<List<AppointmentDto>> GetAppointmentsByDoctorId(int doctorId)
        {
            if (doctorId <= 0)
            {
                throw new ArgumentException("Invalid doctor ID.", nameof(doctorId));
            }
            
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
            
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return appointmentDtos;
        }

        public async Task<Appointment> UpdateAppointmentStatus(Appointment appointment)
        {
        
            if (appointment.StatusId!= 0 && appointment.StatusId != 1)
            {
                throw new ArgumentException("Invalid statusId. Must be 0 (cancelled) or 1 (booked).");
            }

            var existingAppointment = await _appointmentRepository.GetAppointment(appointment.AppointmentId);

            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
            existingAppointment.StatusId = appointment.StatusId;

            return await _appointmentRepository.UpdateAppointmentStatus(existingAppointment);
        }

        public Task<List<Appointment>> GetAppointmentsByHospital(string hospitalName)
        {
            return _appointmentRepository.GetAppointmentsByHospital(hospitalName);
        }
    }
}
