using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDtl> GetPatientById(string patientId);
        Task<PatientDtl> GetPatientByEmail(string email);
        Task<string> GenerateToken(string email);
        Task<bool> UpdatePatientPassword(string patientEmail, string newPassword);

    }
}
