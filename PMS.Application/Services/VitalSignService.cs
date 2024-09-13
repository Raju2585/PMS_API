using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PMS.Application.Services
{
    public class VitalSignService:IVitalSignService
    {
        private readonly IVitalSignRepository _vitalSignRepository;
        public VitalSignService(IVitalSignRepository vitalSignRepository)
        {
            _vitalSignRepository = vitalSignRepository;
        }

        public async Task<VitalSign> CreateVitalSign(int deviceId)
        {
            var random=new Random();
            var vitalSign = new VitalSign
            {
                DeviceId = deviceId,
                HeartRate = random.Next(30, 220),
                OxygenSaturation = random.Next(80, 100),
                Temperature = (float)(95f + (random.NextDouble() * (104f - 95f))),
                BloodPressure = $"{random.Next(60, 250)}/{random.Next(30, 150)}",
                RespiratoryRate = random.Next(6, 40)
            };
            return await _vitalSignRepository.CreateVitalSign(vitalSign);

        }

        public async Task<VitalSign> GetVitalSignByPatient(int patientid)
        {
            
            var vitalsigns=await _vitalSignRepository.GetVitalSignByPatient(patientid);
            return vitalsigns;
            
        }
    }
}
