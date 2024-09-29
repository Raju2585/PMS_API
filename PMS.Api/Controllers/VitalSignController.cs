using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "PATIENT")]
        [HttpGet]
        [Route("GetVitalSigns")]
        public async Task<ActionResult<IEnumerable<VitalSign>>> GetVitalSignByDeviceId(int deviceid)
        {
            var vitalSigns = await _vitalservice.GetVitalSignByDeviceId(deviceid);

            if (vitalSigns == null )
            {
                return NotFound(); 
            }

            return Ok(vitalSigns);
        }
        [Authorize(Roles = "PATIENT")]
        [HttpGet]
        [Route("GetVitalSignsByPatientId")]
        public async Task<ActionResult<IEnumerable<VitalSign>>> GetVitalSignByPatient(string patientId)
        {
            var vitalSigns = await _vitalservice.GetVitalSignByPatient(patientId);

            if (vitalSigns == null)
            {
                return NotFound();
            }

            return Ok(vitalSigns);
        }


    }
}
