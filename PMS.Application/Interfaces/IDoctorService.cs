using PMS.Domain.Entities;
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
        Task<List<Doctor>> GetAllDoctorsDTO();

        Task<Doctor> GetDoctorByID(int doctorId);

        Task<List<Doctor>>  GetDoctorsBySpecialist(string Specialist);
        Task<List<Doctor>> GetDoctorsByHospitalId(int hospitalId);

    }
}
