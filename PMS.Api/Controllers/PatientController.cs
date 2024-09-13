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
        private readonly IDeviceService _deviceService;
        private readonly IVitalSignService _vitalSignService;
        public PatientController(
            IPatientService patientService,
            IDeviceService deviceService,
            IVitalSignService vitalSignService
            )
        {
            _patientService = patientService;
            _deviceService = deviceService;
            _vitalSignService = vitalSignService;
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
        public async Task<ActionResult<PatientRes>> Register(PatientReq patientReq)
        {
            var PatientRes = await _patientService.RegisterPatient(patientReq);
            if (PatientRes.IsSuccess)
            {
                var device = await _deviceService.CreateDevice(PatientRes.PatientEmail);
                var vitalSign = await _vitalSignService.CreateVitalSign(device.DeviceId);
                device.VitalSign = vitalSign;
                return PatientRes;
            }

            return Ok(PatientRes);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("PatientLogin")]
        public async Task<IActionResult> PatientLogin(LoginReq patient)
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
