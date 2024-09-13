using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class HospitalRepository:IHospitalRepository
    {
        private readonly IApplicationDbContext _context;
        public HospitalRepository(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Hospital>> GetAllHospitals()
        {
            return await _context.Hospitals.ToListAsync();
        }
        public async Task<List<Hospital>> GetHospitalByLocation(string location)
        {
            var hospitalsList = await _context.Hospitals.Where(h => h.City == location).OrderBy(s=>s.City).ToListAsync();
            return hospitalsList;
        }

        public async Task<List<Hospital>> GetHospitalByPinCode(int pinCode)
        {
            var hospitalsListByPincode = await _context.Hospitals.Where(s => s.Pincode == pinCode).OrderBy(s => s.City).ToListAsync();
            return hospitalsListByPincode;

        }


    }
}
