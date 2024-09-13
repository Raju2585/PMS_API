using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;

namespace PMS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                return BadRequest("Appointment cannot be null");
            }
            var result = await _appointmentService.ScheduleAppointment(appointmentDto);
            return Ok(result);
        }

        [HttpGet]
        [Route("Retrieve/{id:int}")]
        public async Task<IActionResult> RetrieveAppointmentById(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointment(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPut]
        [Route("Modify/{id:int}")]
        public async Task<IActionResult> ModifyAppointment(int id, [FromBody] RequestAppointmentDto appointment)
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

        [HttpGet]
        [Route("patient/{patientId:int}")]
        public async Task<IActionResult> RetrieveAppointmentsByPatientId(int patientId)
        {
            var appointment = await _appointmentService.GetAppointmentsByPatientId(patientId);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("doctor/{doctorId:int}")]
        public async Task<IActionResult> RetrieveAppointmentsByDoctorId(int doctorId)
        {
            var appointment = await _appointmentService.GetAppointmentsByDoctorId(doctorId);
            return Ok(appointment);
        }

        [HttpPut]
        [Route("update-status/{appointmentId:int}")]
        public async Task<IActionResult> UpdateStatus(int appointmentId, [FromQuery] int status)
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
                    StatusId = status
                };

                
                var updatedAppointment = await _appointmentService.UpdateAppointmentStatus(appointment);

                
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
        [HttpGet("hospital/{hospitalName}")]
        public async Task<IActionResult> RetrieveAppointmentsByHospital(string hospitalName)
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

    }
}



