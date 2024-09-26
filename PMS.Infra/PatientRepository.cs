﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Patient> GetPatientById(int patientId)
        {
            var patient = _applicationDbContext.Patients.FirstOrDefault(p => p.PatientId == patientId);
            return patient;
        }
        public async Task<List<Patient>> GetAllPatients()
        {
            return await _applicationDbContext.Patients.ToListAsync();
        }
        public async Task<Patient> GetPatientByEmail(string email)
        {
            var patient = _applicationDbContext.Patients.FirstOrDefault(p => p.PatientEmail == email);
            return patient;
        }
        public async Task<bool> CheckIfPatientExisted(Patient patient)
        {
            var isExisted = _applicationDbContext.Patients.FirstOrDefault(p => p.PatientEmail == patient.PatientEmail);
            if (isExisted == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> RegisterPatient(Patient patient)
        {
            var isExisted = await CheckIfPatientExisted(patient);
            if (!isExisted)
            {
                await _applicationDbContext.Patients.AddAsync(patient);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
