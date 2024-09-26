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
    public interface IDeviceService
    {
        Task<Device> CreateDevice(int patientId, string email, string password);
        Task<DeviceRes> AddDevice(DeviceReq deviceReq);
    }
}
