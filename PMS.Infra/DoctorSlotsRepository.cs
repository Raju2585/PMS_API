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
    public class DoctorSlotsRepository:IDoctorSlotsRepository
    {
        public readonly IApplicationDbContext _applicationDbContext;
        public DoctorSlotsRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Doctor_Slots> GetDoctorSlotsByDate(int DoctorId, DateOnly date)
        {
            var slots = await _applicationDbContext.Doctor_Slots.FirstOrDefaultAsync(s=>s.date==date && s.DoctorId==DoctorId);
            return slots;
        }
        public async Task<int> UpdateDoctorSlots(Doctor_Slots doctor_Slots)
        {
            _applicationDbContext.Doctor_Slots.Update(doctor_Slots);
            return await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<Doctor_Slots> AddDoctorSlots(Doctor_Slots doctor_Slots)
        {
            await _applicationDbContext.Doctor_Slots.AddAsync(doctor_Slots);
            await _applicationDbContext.SaveChangesAsync();
            return doctor_Slots;
        }
    }
}
