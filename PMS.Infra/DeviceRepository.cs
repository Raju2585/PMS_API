using Microsoft.EntityFrameworkCore;
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
        public async Task<Device> CreateDevice(int patientId, string email, string password)
        {
            var device = new Device
            {
                PatientId = patientId,
                Email = email,
                Password = password
            };
            
            await _applicationDbContext.Devices.AddAsync(device);
            await _applicationDbContext.SaveChangesAsync();
            return device;
        }
        public async Task<Device> GetDeviceByEmail(string email)
        {
            return await _applicationDbContext.Devices.FirstOrDefaultAsync(d=>d.Email==email);
        }
    }
}
