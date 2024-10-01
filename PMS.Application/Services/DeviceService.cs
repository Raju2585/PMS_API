using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System.Linq;

namespace PMS.Application.Services
{
    public class DeviceService:IDeviceService
    {
        public readonly IDeviceRepository _deviceRepository;
        private readonly IPatientRepository _patientRepository;
        public DeviceService(IDeviceRepository deviceRepository, IPatientRepository patientRepository)
        {
            _deviceRepository = deviceRepository;
            _patientRepository = patientRepository;
        } 
        public async Task<Device> CreateDevice(string patientId,string email,string password)
        {
            var device= await _deviceRepository.CreateDevice(patientId, email, password);
            return device;
        }
        public async Task<Device> GetDeviceByEmail(string email)
        {
            try
            {
                var devices = await _deviceRepository.GetDeviceByEmail(email);
                return devices;
            }
            catch (Exception ex)
            {
                return null;
            }
             
        }
        public async Task<DeviceRes> AddDevice(DeviceReq deviceReq)
        {
            var device= await GetDeviceByEmail(deviceReq.Email);
            var deviceRes=new DeviceRes();  
            if(device!=null)
            {
                if(device.Password==deviceReq.Password)
                {
                    deviceRes.Device=device;
                    deviceRes.IsSuccess=true;
                    deviceRes.IsDeviceExisted=true;
                    return deviceRes;
                }
                else
                {
                    deviceRes.IsSuccess = false;
                    deviceRes.Error = "Invalid credentials";
                    return deviceRes;
                }
            }

            var result = await CreateDevice(deviceReq.Id, deviceReq.Email, deviceReq.Password);
            deviceRes.Device = result;
            deviceRes.IsSuccess=true ;
            return deviceRes;

        }
    }
}
