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

        [HttpGet]
        [Route("GetPatientById")]
        public async Task<ActionResult<PatientDtl>> GetPatientById(int patientId)
        {
            var patientDetail = await _patientService.GetPatientById(patientId);
            if (patientDetail == null)
            {
                return NotFound(); 
            }

            return Ok(patientDetail);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllPatients")]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            var patients= await _patientService.GetAllPatientDtls();
            return Ok(patients);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(PatientReq patientReq)
        {
            var PatientRes = await _patientService.RegisterPatient(patientReq);
            if (PatientRes.IsSuccess)
            {
                return Ok(PatientRes);
            }

            return Ok(PatientRes);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginReq patient)
        {
            IActionResult response = Unauthorized();
            var loginRes = await _patientService.Login(patient);
            if (loginRes != null)
            {
                return Ok(loginRes);
            }
            return response;
        }

    }
}
