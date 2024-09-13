using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctors();

        Task<Doctor> GetDoctorById(int id);

        Task<List<Doctor>> GetDoctorsBySpecialist(string Specialist);
    }
}
