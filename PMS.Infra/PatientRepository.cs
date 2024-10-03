using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class PatientRepository:IPatientRepository
    {
        public readonly IApplicationDbContext _applicationDbContext;
        public readonly IMapper _mapper;
        public PatientRepository(IApplicationDbContext applicationDbContext,IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PatientDtl> GetPatientById(string patientId)
        {
            var patient = await _applicationDbContext.Patients
                .FirstOrDefaultAsync(p => p.Id == patientId);
            var result = _mapper.Map<PatientDtl>(patient);
            return result;
        }
        public async Task<Patient> GetPatientEmail(string email)
        {
            return await _applicationDbContext.Patients
                .FirstOrDefaultAsync(p => p.Email == email);
        }
        public async Task<bool> UpdatePatientPassword(string patientEmail, string newPassword)
        {
            var patient = await _applicationDbContext.Patients
                .SingleOrDefaultAsync(p => p.Email.ToLower() == patientEmail);

            if (patient == null)
            {
                return false; // User not found
            }

            // Hash the new password
            patient.PasswordHash = HashPassword(newPassword); // Use the new password directly

            // Save the changes
            await _applicationDbContext.SaveChangesAsync();
            return true; // Indicate success
        }


        private string HashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

       

    }
}
