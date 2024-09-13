using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();
        Task<bool> RegisterPatient(Patient patient);
        Task<bool> CheckIfPatientExisted(Patient patient);
        Task<Patient> GetPatientByEmail(string email);
    }
}
