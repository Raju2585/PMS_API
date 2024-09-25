using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IDoctorSlotsRepository
    {
        Task<Doctor_Slots> GetDoctorSlotsByDate(int DoctorId,DateOnly date);
        Task<int> UpdateDoctorSlots(Doctor_Slots doctor_Slots);
        Task<Doctor_Slots> AddDoctorSlots(Doctor_Slots doctor_Slots);
    }
}
