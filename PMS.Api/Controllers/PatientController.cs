using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        public readonly IPatientService _patientService;
        public PatientController(
            IPatientService patientService
            )
        {
            _patientService = patientService;
        }
        [Authorize(Roles = "PATIENT")]
        [HttpGet]
        [Route("GetPatientById")]
        public async Task<ActionResult<PatientDtl>> GetPatientById(string patientId)
        {
            var patientDetail = await _patientService.GetPatientById(patientId);
            if (patientDetail == null)
            {
                return NotFound();
            }

            return Ok(patientDetail);
        }
        [HttpGet]
        [Route("GetPatientByEmail")]
        public async Task<ActionResult<Patient>> GetPatientByEmail(string email)
        {
            var patient = await _patientService.GetPatientByEmail(email);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }
            return Ok(patient);
        }
        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<ActionResult<bool>> UpdatePassword(string email,string newPassword)
        {
            if(string.IsNullOrWhiteSpace(email)|| string.IsNullOrWhiteSpace(newPassword))
            {
                return BadRequest("Invalid request data");
            }

            var patientDtl = await _patientService.GetPatientByEmail(email);
            if (patientDtl == null)
            {
                return NotFound("Patient not found");
            }

            patientDtl.PasswordHash = newPassword;
            var isUpdated = await _patientService.UpdatePatientPassword(email, newPassword);

            if (!isUpdated)
            {
                return StatusCode(500, "Password update failed");
            }
            return Ok(true);
        }

       

    }
}
