using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorSlotsRepository _doctorSlotsRepository;

        public DoctorService(
            IDoctorRepository doctorRepository,
            IConfiguration configuration, 
            IMapper mapper,
            IDoctorSlotsRepository doctorSlotsRepository)
        {

            _doctorRepository = doctorRepository;
            _doctorSlotsRepository = doctorSlotsRepository;
        }
        public async Task<List<DoctorDTO>> GetAllDoctorsDTO()
        {
            try
            {
                var doctors = await _doctorRepository.GetAllDoctors();

                if (doctors == null || !doctors.Any())
                {
                    return new List<DoctorDTO>();
                }

                return doctors;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occured while retrieving docts.", ex.Message);
            }
        }
        public async Task<Doctor> GetDoctorByID(int doctorId)
        {
            try
            {
                //var doctorDetails = await _doctorRepository.GetDoctorById(doctorId);
                if (doctorId <= 0)
                {
                    throw new ArgumentException("Invalid Doctor ID");
                }
                var doctorDetails = await _doctorRepository.GetDoctorById(doctorId);


                return doctorDetails;


            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occured while retrieving the doctor detials", ex.Message);

            }
        }
        public async Task<List<Doctor>> GetDoctorsBySpecialist(string Specialist)
        {
            try
            {
                if (string.IsNullOrEmpty(Specialist))
                {
                    return new List<Doctor>();
                }
                var doctors = await _doctorRepository.GetDoctorsBySpecialist(Specialist);


                return doctors;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occured while retrieving the doctory details by specailist", ex.Message);
            }
        }
        public async Task<List<Doctor>> GetDoctorsByHospitalId(int hospitalId)
        {
            try
            {
                if (hospitalId <= 0)
                {
                    throw new ArgumentException("Invalid Hospital Id");
                }
                var doctors = await _doctorRepository.GetDoctorsByHospitalId(hospitalId);
                return doctors;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Doctor> AddDoctors(string Doctorname, string email, string specialization, string contact, decimal consultationFee, bool isAvailable, int hospitalId, IFormFile? file)
        {
            try
            {
                var doctor = new Doctor
                {
                    DoctorName = Doctorname,
                    DoctorEmail = email,
                    Specialization = specialization,
                    ContactNumber = contact,
                    ConsultationFee = consultationFee,
                    IsAvailable = isAvailable,
                    HospitalId = hospitalId,


                };
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        doctor.Image = memoryStream.ToArray();
                    }

                }
                var Doctors = await _doctorRepository.AddDoctorAsync(doctor);
                return Doctors;
            }catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                // Log the error or throw a custom exception
                throw new Exception($"An error occurred: {innerException}");

            }
        }
        public async Task<Doctor_Slots> GetDoctorSlotsByDate(int DoctorId, DateOnly date)
        {
            var existedSlots= await _doctorSlotsRepository.GetDoctorSlotsByDate(DoctorId,date);
            if (existedSlots == null)
            {
                var newSlots = new Doctor_Slots
                {
                    DoctorId = DoctorId,
                    date = date
                };
                return newSlots;
            }
            return existedSlots;
        }

        public async Task<int> ReleaseDoctorSlot(int DoctorId, DateTime slotDate)
        {
            var date=DateOnly.FromDateTime(slotDate); 
            var time =slotDate.TimeOfDay;
            var existedSlots = await _doctorSlotsRepository.GetDoctorSlotsByDate(DoctorId, date);
            if (existedSlots != null)
            {
                switch (time)
                {
                    case var t when t == new TimeSpan(10, 00, 00):
                        if (existedSlots.Slot_1)
                        {
                            existedSlots.Slot_1 = false;
                        }
                        break;

                    case var t when t == new TimeSpan(11, 00, 00):
                        if (existedSlots.Slot_2)
                        {
                            existedSlots.Slot_2 = false;
                        }
                        break;

                    case var t when t == new TimeSpan(14, 00, 00):
                        if (existedSlots.Slot_3)
                        {
                            existedSlots.Slot_3 = false;
                        }
                        break;

                    case var t when t == new TimeSpan(15, 00, 00):
                        if (existedSlots.Slot_4)
                        {
                            existedSlots.Slot_4 = false;
                        }
                        break;

                    case var t when t == new TimeSpan(16, 00, 00):
                        if (existedSlots.Slot_5)
                        {
                            existedSlots.Slot_5 = false;
                        }
                        break;

                    default:
                        return 0;

                }
            }
            else
            {
                return 0;
            }
            return await _doctorSlotsRepository.UpdateDoctorSlots(existedSlots);
        }


    }
}