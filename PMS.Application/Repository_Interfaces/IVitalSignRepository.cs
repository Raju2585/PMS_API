using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IVitalSignRepository
    {
        Task<VitalSign> CreateVitalSign(VitalSign vitalSign);
        Task<VitalSign> GetVitalSignByPatient(int patientid);
    }
}
