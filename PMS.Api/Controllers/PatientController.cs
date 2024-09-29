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

       

    }
}
