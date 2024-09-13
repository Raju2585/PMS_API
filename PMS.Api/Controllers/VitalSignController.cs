using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Application.Services;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalSignController : ControllerBase
    {
        private IVitalSignService _vitalservice;
        public VitalSignController(IVitalSignService vitalservice)
        {
            _vitalservice = vitalservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VitalSign>>> GetVitalSignByPatient(int patientId)
        {
            var vitalSigns = await _vitalservice.GetVitalSignByPatient(patientId);

            if (vitalSigns == null )
            {
                return NotFound(); 
            }

            return Ok(vitalSigns);
        }


    }
}
