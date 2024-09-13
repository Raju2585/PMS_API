using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class HospitalService:IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        public HospitalService(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }
        public async Task<List<Hospital>> GetAllHospitals()
        {
            return await _hospitalRepository.GetAllHospitals();
        }
        public async Task<List<Hospital>> GetHospitalByLocation(string location)
        {
            return await _hospitalRepository.GetHospitalByLocation(location);
        }
        public async Task<List<Hospital>> GetHospitalByPinCode(int pinCode)
        {
            return await _hospitalRepository.GetHospitalByPinCode(pinCode);
        }

    }
}
