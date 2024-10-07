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
    public class DoctorRepository : IDoctorRepository
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
            var doctorDetails = await _context.Doctors.FirstOrDefaultAsync(s => s.DoctorId == id);
            return doctorDetails;

        }
        public async Task<List<Doctor>> GetDoctorsBySpecialist(string Specialist)
        {
            var doctorBySpecialst = await _context.Doctors.Where(s => s.Specialization == Specialist).ToListAsync();
            return doctorBySpecialst;
        }
        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();
            return doctor;
        }
        public async Task<List<Doctor>> GetDoctorsByHospitalId(int hospitalId)
        {
            var doctorsByHospital = await _context.Doctors.Where(s => s.HospitalId == hospitalId && s.IsAvailable == true).ToListAsync();
            return doctorsByHospital;
        }

    }
}