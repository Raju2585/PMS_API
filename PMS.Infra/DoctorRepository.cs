using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
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

        public async Task<List<DoctorDTO>> GetAllDoctors()
        {
            return await _context.Doctors
                .Select(doctor => new DoctorDTO
                {
                    
                    DoctorId=doctor.DoctorId,
                    DoctorEmail=doctor.DoctorEmail,
                    HospitalId=doctor.HospitalId,
                    DoctorName = doctor.DoctorName,
                    Specialization = doctor.Specialization,
                    ConsultationFee = doctor.ConsultationFee,
                    Image = doctor.Image,
                    HospitalName = doctor.Hospital.HospitalName,
                    City = doctor.Hospital.City,
                    ContactNumber=doctor.ContactNumber,
                    // Map other properties as needed
                })
                .ToListAsync();
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