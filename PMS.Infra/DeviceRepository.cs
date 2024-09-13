using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class DeviceRepository:IDeviceRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeviceRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Device> CreateDevice(Patient patient)
        {
            var device = new Device
            {
                DeviceName = patient.DeviceName,
                PatientId = patient.PatientId,
                Patient = patient
            };
            
            await _applicationDbContext.Devices.AddAsync(device);
            await _applicationDbContext.SaveChangesAsync();
            return device;
        }
    }
}
