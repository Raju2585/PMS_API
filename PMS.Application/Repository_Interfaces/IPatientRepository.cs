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
        Task<PatientDtl> GetPatientById(string patientId);
    }
}
