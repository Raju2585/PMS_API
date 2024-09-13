using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IVitalSignService
    {
        Task<VitalSign> CreateVitalSign(int deviceId);
        Task<VitalSign> GetVitalSignByPatient(int patientid);
    }
}
