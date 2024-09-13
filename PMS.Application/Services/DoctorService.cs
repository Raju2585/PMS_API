using AutoMapper;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository doctorRepository,IConfiguration configuration,IMapper mapper) {

            _doctorRepository= doctorRepository;
            _configuration= configuration;
            _mapper = mapper;
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
                var doctorsList = _mapper.Map<List<DoctorDTO>>(doctors);
                return doctorsList;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occured while retrieving docts.",ex.Message);
            }
        }
        public async Task<DoctorDTO> GetDoctorByID(int doctorId)
        {
            try
            {
                //var doctorDetails = await _doctorRepository.GetDoctorById(doctorId);
                if (doctorId <= 0)
                {
                    throw new ArgumentException("Invalid Doctor ID");
                }
                var doctorDetails = await _doctorRepository.GetDoctorById(doctorId);

                if(doctorDetails == null)
                {
                    return null;
                }
                var doctorDto = _mapper.Map<DoctorDTO>(doctorDetails);
                return doctorDto;

                
            }
            catch(Exception ex) 
            {
                    throw new ArgumentException("Error occured while retrieving the doctor detials", ex.Message);

            }
        }
        public async Task<List<DoctorDTO>> GetDoctorsBySpecialist(string Specialist)
        {
            try
            {
                if (string.IsNullOrEmpty(Specialist))
                {
                    return new List<DoctorDTO>();
                }
                var doctors = await _doctorRepository.GetDoctorsBySpecialist(Specialist);

                var doctorDtos = _mapper.Map<List<DoctorDTO>>(doctors);

                return doctorDtos;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occured while retrieving the doctory details by specailist",ex.Message);
            }
        }
    }
}
