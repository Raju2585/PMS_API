﻿using Microsoft.AspNetCore.Http;
using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllDoctorsDTO();

        Task<DoctorDTO> GetDoctorByID(int doctorId);

        Task<List<DoctorDTO>>  GetDoctorsBySpecialist(string Specialist);

        Task<DoctorDTO> AddDoctorAsync(string DoctorName, string Specialization, Decimal ConsultationFee, bool isAvailable, IFormFile image);

    }
}
