using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IMedicalhistoryService
    {
        Task<MedicalHistoryDTOs> AddMedicalHistory(MedicalHistoryDTOs medicalHistorydto);
        
        Task<List<MedicalHistoryDTOs>> GetMedicalHistoryByPatient(int patientId);
    }
}
