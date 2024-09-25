using PMS.Domain.Entities;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDtl> GetPatientById(int patientId);
        Task<List<Patient>> GetAllPatients();
        Task<bool> RegisterPatient(Patient patient);
        Task<bool> CheckIfPatientExisted(Patient patient);
        Task<Patient> GetPatientByEmail(string email);
        Task<Patient> GetPatientById(int patientId);
    }
}
