﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository, IConfiguration configuration, IMapper mapper)
        {

            _doctorRepository = doctorRepository;

        }
        public async Task<List<Doctor>> GetAllDoctorsDTO()
        {
            try
            {
                var doctors = await _doctorRepository.GetAllDoctors();

                if (doctors == null || !doctors.Any())
                {
                    return new List<Doctor>();
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
        public async Task<Doctor> AddDoctors(Doctor doctor)
        {
            try
            {
                var doctors = await _doctorRepository.AddDoctorAsync(doctor);
                return doctors;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


    }
}