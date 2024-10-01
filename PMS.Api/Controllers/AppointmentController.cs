using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System.Net.Mail;
using System.Net;
using PMS.Domain.Entities.Response;
using AutoMapper;

namespace PMS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService, IEmailService emailService, INotificationService notificationService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _emailService = emailService;
            _notificationService = notificationService;
            _mapper = mapper;

        }
        [Authorize(Roles ="PATIENT")]
        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest("Appointment cannot be null");
            }
            var result = await _appointmentService.ScheduleAppointment(appointmentDto);
            return Ok(result);
        }
        [Authorize(Roles = "PATIENT")]
        [HttpGet]
        [Route("GetAppointment/{id:int}")]
        public async Task<IActionResult> GetAppointment(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointment(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        [Authorize(Roles = "PATIENT,RECEPTIONIST")]
        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var updatedAppointment = await _appointmentService.UpdateAppointment(id, appointment);
            if (updatedAppointment == null)
            {
                return NotFound();
            }

            return Ok(updatedAppointment);
        }

        [Authorize(Roles = "PATIENT")]
        [HttpGet]
        [Route("GetAppointmentByPatientId/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(string patientId)
        {
            var appointment = await _appointmentService.GetAppointmentsByPatientId(patientId);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("GetAppointmentByDoctorId/{doctorId:int}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
        {
            var appointment = await _appointmentService.GetAppointmentsByDoctorId(doctorId);
            return Ok(appointment);
        }

        [Authorize(Roles = "PATIENT,RECEPTIONIST")]
        [HttpPut]
        [Route("UpdateStatus/{appointmentId:int}")]
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, [FromQuery] int status)
        {

            if (status != 0 && status != 1)
            {
                return BadRequest("Invalid statusId. Must be 0 (cancelled) or 1 (booked).");
            }

            try
            {
                var appointment = new Appointment
                {
                    AppointmentId = appointmentId,
                    StatusId = 1
                };


                var updatedAppointment = await _appointmentService.UpdateAppointmentStatus(appointment);


                if (updatedAppointment == null)
                {
                    return NotFound("Appointment not found.");
                }
                if (status == 1)
                {
                    var doctor = await _doctorService.GetDoctorByID(updatedAppointment.DoctorId);
                    var patient = await _patientService.GetPatientById(updatedAppointment.Id);

                    if (doctor == null || patient == null)
                    {
                        return NotFound("Doctor or Patient not found.");
                    }

                    var emailBody = await _emailService.GenerateEmailBody(patient, updatedAppointment, doctor);

                    _emailService.SendEmailNotification(patient.Email, "Appointment Confirmation", emailBody);

                    _notificationService.CreateNotificationForAppointment(updatedAppointment);

                }




                return Ok(updatedAppointment);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {

                return NotFound("Appointment not found.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "RECEPTIONIST")]
        [HttpGet("GetHospitalName/{hospitalName}")]
        public async Task<IActionResult> GetAppointmentsByHospital(string hospitalName)
        {
            if (string.IsNullOrWhiteSpace(hospitalName))
            {
                return BadRequest("Hospital name cannot be null or empty");
            }

            try
            {
                var appointments = await _appointmentService.GetAppointmentsByHospital(hospitalName);
                if (appointments == null || appointments.Count == 0)
                {
                    return NotFound("No appointments found for the specified hospital");
                }
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "PATIENT, RECEPTIONIST")]
        [HttpDelete]
        [Route("Cancel/{appointmentId:int}")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            try
            {
                
                var updatedAppointment = await _appointmentService.UpdateAppointmentStatus(appointmentId, 0);

                if (updatedAppointment == null)
                {
                    return NotFound("Appointment not found.");
                }

                return Ok(updatedAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Appointment not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}



