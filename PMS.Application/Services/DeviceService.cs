using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
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
        public async Task<Device> CreateDevice(string patientEmail)
        {
            var patientList=await _patientRepository.GetAllPatients();
            var patient=patientList.FirstOrDefault(p=>p.PatientEmail==patientEmail);
            if (patient!=null)
            {
                var device= await _deviceRepository.CreateDevice(patient);
                patient.Device=device;
                return device;
            }
            return new Device();
        }
    }
}
