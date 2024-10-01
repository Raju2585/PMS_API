using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
using System.Net.WebSockets;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("Get/All/Doctors")]
        public async Task<ActionResult<List<Doctor>>> GetAllDoctors()
        {
            try
            {
                var doctorsList = await _doctorService.GetAllDoctorsDTO();
                return Ok(doctorsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Get/DoctorById/{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorDTOAsync(int id)
        {
            try
            {
                var doctorDetails = await _doctorService.GetDoctorByID(id);
                return Ok(doctorDetails);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("Get/Doctor/{specailist}")]
        public async Task<ActionResult<Doctor>> GetDoctorBySpecialist(string specailist)
        {
            try
            {
                var doctorsListBySpecialist = await _doctorService.GetDoctorsBySpecialist(specailist);
                return Ok(doctorsListBySpecialist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Get/Doctor/HospitalId/{hospitalId}")]
        public async Task<ActionResult<Doctor>> GetDoctorsByHospitals(int hospitalId)
        {
            try
            {
                var doctors = await _doctorService.GetDoctorsByHospitalId(hospitalId);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Add/Doctors")]
        public async Task<ActionResult<Doctor>> AddDoctors([FromForm] string Doctorname, string email, string specialization, string contact, decimal consultationFee, bool isAvailable, int hospitalId, IFormFile? file) 
        {
            try
            {
                var doctors = await _doctorService.AddDoctors(Doctorname, email, specialization, contact, consultationFee, isAvailable, hospitalId, file); ;
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetDoctorSlotsByDate")]
        public async Task<ActionResult<Doctor_Slots>> GetDoctorSlotsByDate(int DoctorId,DateTime date)
        {
            try
            {
                var dateOnly=DateOnly.FromDateTime(date);
                return await _doctorService.GetDoctorSlotsByDate(DoctorId,dateOnly);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    }
  
