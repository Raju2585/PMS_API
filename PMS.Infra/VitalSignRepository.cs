using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class VitalSignRepository: IVitalSignRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VitalSignRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<VitalSign> CreateVitalSign(VitalSign vitalSign)
        {
            await _applicationDbContext.VitalSigns.AddAsync(vitalSign);
            await _applicationDbContext.SaveChangesAsync();
            return vitalSign;
        }

        public async Task<VitalSign> GetVitalSignByPatient(int patientId)
        {
            // Retrieve DeviceIds for the given patientId
            var deviceIds = await _applicationDbContext.Devices
                .Where(d => d.PatientId == patientId)
                .Select(d => d.DeviceId)
                .ToListAsync();

            

            // Retrieve the first VitalSign associated with any of the retrieved DeviceIds
            var vitalSign = await _applicationDbContext.VitalSigns
                .Where(v => deviceIds.Contains(v.DeviceId))
                .FirstOrDefaultAsync(); // Retrieves a single VitalSign

            return vitalSign;
        }

    }
}
