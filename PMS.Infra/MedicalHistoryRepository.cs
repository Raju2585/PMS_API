using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class MedicalHistoryRepository:IMedicalHistoryRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public MedicalHistoryRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<MedicalHistory> AddMedicalHistory(MedicalHistory medicalHistory)
        {
            // Check if the medicalHistory  is null
            if (medicalHistory == null)
            {
                throw new ArgumentNullException(nameof(medicalHistory));
            }
            var isData = _applicationDbContext.MedicalHistories.Where(d => d.HistoryId == medicalHistory.HistoryId).Any();
            if (isData)
            {
                return null;
            }
            _applicationDbContext.MedicalHistories.Add(medicalHistory);
            _applicationDbContext.SaveChanges();
            return medicalHistory;

        }

       

        public async Task<List<MedicalHistory>> GetMedicalHistoryByPatient(string patientId)
        { 

            return await _applicationDbContext.MedicalHistories
                .Where(m => m.Id == patientId)
                .ToListAsync();
        }

       

    }
}
