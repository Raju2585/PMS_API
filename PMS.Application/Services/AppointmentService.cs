using AutoMapper;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System.Net.Mail;
using System.Net;
using PMS.Domain.Entities.Response;

namespace PMS.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorSlotsRepository _slotRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IMapper mapper, 
            IDoctorRepository doctorRepository,
            IDoctorSlotsRepository slotRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _slotRepository = slotRepository;
        }

        public async Task<AppointmentRes> ScheduleAppointment(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
                throw new ArgumentNullException(nameof(appointmentDto));

            if (appointmentDto.AppointmentDate < DateTime.UtcNow)
                throw new ArgumentException("Appointment date cannot be in the past.");
            var doctor = await _doctorRepository.GetDoctorById(appointmentDto.DoctorId);
            var response = "";
            if (doctor.IsAvailable)
            {
                var date = DateOnly.FromDateTime(appointmentDto.AppointmentDate);
                var time = appointmentDto.AppointmentDate.TimeOfDay;
                var doctorSlots = await _slotRepository.GetDoctorSlotsByDate(doctor.DoctorId, date);
                if (doctorSlots != null)
                {
                    switch (time)
                    {
                        case var t when t == new TimeSpan(10, 0, 0):
                            if (!doctorSlots.Slot_1)
                            {
                                doctorSlots.Slot_1 = true;
                            }
                            break;

                        case var t when t == new TimeSpan(11, 0, 0):
                            if (!doctorSlots.Slot_2)
                            {
                                doctorSlots.Slot_2 = true;
                            }
                            break;

                        case var t when t == new TimeSpan(14, 0, 0):
                            if (!doctorSlots.Slot_3)
                            {
                                doctorSlots.Slot_3 = true;
                            }
                            break;

                        case var t when t == new TimeSpan(15, 0, 0):
                            if (!doctorSlots.Slot_4)
                            {
                                doctorSlots.Slot_4 = true;
                            }
                            break;

                        case var t when t == new TimeSpan(16, 0, 0):
                            if (!doctorSlots.Slot_5)
                            {
                                doctorSlots.Slot_5 = true;
                            }
                            break;

                        default:
                            response += "No slots are available";
                            break;
                    }
                    if (response == "")
                    {
                        await _slotRepository.UpdateDoctorSlots(doctorSlots);
                    }
                    else
                    {
                        return new AppointmentRes { IsSuccess = false, Error = response };
                    }
                }
                else
                {
                    var newDoctorSlots = new Doctor_Slots
                    {
                        DoctorId = appointmentDto.DoctorId,
                        date = date
                    };
                    switch (time)
                    {
                        case var t when t == new TimeSpan(10, 0, 0):
                            newDoctorSlots.Slot_1 = true;
                            break;

                        case var t when t == new TimeSpan(11, 0, 0):
                            newDoctorSlots.Slot_2 = true;
                            break;
                        case var t when t == new TimeSpan(14, 0, 0):
                            newDoctorSlots.Slot_3 = true;
                            break;
                        case var t when t == new TimeSpan(15, 0, 0):
                            newDoctorSlots.Slot_4 = true;
                            break;
                        case var t when t == new TimeSpan(16, 0, 0):
                            newDoctorSlots.Slot_5 = true;
                            break;

                        default:
                            response += "No slots are available";
                            break;
                    }
                    if (response == "")
                    {
                        await _slotRepository.AddDoctorSlots(newDoctorSlots);
                    }
                    else
                    {
                        return new AppointmentRes { IsSuccess = false, Error = response };
                    }

                }
            }
            else
            {
                return new AppointmentRes { IsSuccess = false, Error = "Doctor is not avalable" };
            }

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            var scheduledAppointment = await _appointmentRepository.ScheduleAppointment(appointment);
            var scheduledAppointmentDto = _mapper.Map<AppointmentDto>(scheduledAppointment);

            return new AppointmentRes { IsSuccess = true };
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


        public async Task<AppointmentDto> UpdateAppointment(int appointmentId, AppointmentDto updatedAppointmentDto)
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


        public async Task<List<AppointmentDto>> GetAppointmentsByPatientId(string patientId)
        {
            if (patientId.Length <= 0)
            {
                throw new ArgumentException("Invalid patient ID.", nameof(patientId));
            }

            var appointments = await _appointmentRepository.GetAppointmentsByPatientId(patientId);


            //var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return appointments;
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
        
            if (!(appointment.StatusId>=-1) && !(appointment.StatusId <= 2))
            {
                throw new ArgumentException("Invalid statusId. Must be 0 (cancelled) or 1 (booked) or 2(Confirmed).");
            }

            var existingAppointment = await _appointmentRepository.GetAppointment(appointment.AppointmentId);

            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
            existingAppointment.StatusId = appointment.StatusId;

            //sending email
         
        

            return await _appointmentRepository.UpdateAppointmentStatus(existingAppointment);
        }

        public Task<List<AppointmentDto>> GetAppointmentsByHospital(string hospitalName,int statusId)
        {
            return _appointmentRepository.GetAppointmentsByHospital(hospitalName, statusId);
        }

        public async Task<Appointment> UpdateAppointmentStatus(int appointmentId, int statusId)
        {
           
            if (statusId != 0 && statusId != 1)
            {
                throw new ArgumentException("Invalid statusId. Must be 0 (cancelled) or 1 (booked).");
            }
            
            var existingAppointment = await _appointmentRepository.GetAppointment(appointmentId);
            
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }
            
            existingAppointment.StatusId = statusId;
            
            return await _appointmentRepository.UpdateAppointmentStatus(existingAppointment);
        }
        public async Task<List<AppointmentDto>> GetAppointmentsByHospital(string hospitalName)
        {
            List<AppointmentDto> appointmentsList=new List<AppointmentDto>();
            try
            {
                appointmentsList = await _appointmentRepository.GetAppointmentsByHospital(hospitalName);
            }
            catch(Exception ex)
            {
                return appointmentsList;
            }
            return appointmentsList;
        }

    }
}
