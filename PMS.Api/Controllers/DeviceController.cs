using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System.Reflection.Metadata.Ecma335;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IVitalSignService _vitalSignService;
        public DeviceController(IDeviceService deviceService, IVitalSignService vitalSignService)
        {
            _deviceService = deviceService;
            _vitalSignService = vitalSignService;
        }

        [HttpPost("AddDevice")]
        public async Task<ActionResult> AddDevice(DeviceReq deviceReq)
        {
                var deviceRes=new DeviceRes();
            if (deviceReq != null)
            {
                var device=await _deviceService.AddDevice(deviceReq);
                if(device.IsSuccess && device.IsDeviceExisted)
                {
                    var vitalsigns = await _vitalSignService.GetVitalSignByDeviceId(device.Device.DeviceId);
                    deviceRes.IsSuccess = true;
                    deviceRes.VitalSign=vitalsigns;
                    return Ok(deviceRes);
                }
                else if(device.IsSuccess)
                {
                    var vitalsigns=await _vitalSignService.CreateVitalSign(device.Device.DeviceId);
                    deviceRes.IsSuccess = true;
                    deviceRes.VitalSign = vitalsigns;
                    return Ok(deviceRes);
                }
            }
            deviceRes.IsSuccess=false;
            deviceRes.Error = "Device data required";
            return BadRequest(deviceRes);
        }
        
    }
}
