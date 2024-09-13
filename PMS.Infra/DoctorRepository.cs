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
    public class DoctorRepository:IDoctorRepository
    {
      private readonly IApplicationDbContext _context;

        public DoctorRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctorDetails= await  _context.Doctors.FirstOrDefaultAsync(s=>s.HospitalId==id);
            return  doctorDetails;

        }
        public async Task<List<Doctor>> GetDoctorsBySpecialist(string Specialist)
        {
            var doctorBySpecialst=await _context.Doctors.Where(s=>s.Specialization==Specialist).ToListAsync();
            return doctorBySpecialst;
        }
    }
}
