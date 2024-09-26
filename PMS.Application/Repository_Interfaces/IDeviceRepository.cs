﻿using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IDeviceRepository
    {
        Task<Device> CreateDevice(int patientId, string email, string password);
        Task<Device> GetDeviceByEmail(string email);
    }
}
