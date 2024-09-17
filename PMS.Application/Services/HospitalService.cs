using Microsoft.AspNetCore.Http;
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
        public async Task<Hospital> GetHospitalById(int id)
        {
            return await _hospitalRepository.GetHospitalById(id);
        }
        public async Task<Hospital> AddHospitalAsync(string hospitalName, string city, int pincode, IFormFile? imageFile)
        {
            var hospital = new Hospital
            {
                HospitalName = hospitalName,
                City = city,
                Pincode = pincode
            };

            if (imageFile != null && imageFile.Length > 0)
            {
               /* if (imageFile.Length > 5 * 1024 * 1024) // Check image size (e.g., max 5MB)
                {
                    throw new ArgumentException("Image size exceeds the maximum allowed size of 5MB", nameof(imageFile));
                }*/

                // Convert the image file to byte array
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    hospital.HospitalImage = memoryStream.ToArray();
                }
            }

            // Add the hospital using the repository
            var addedHospital = await _hospitalRepository.AddHospitalAsync(hospital);

            return addedHospital;

        }
       
    }
}
