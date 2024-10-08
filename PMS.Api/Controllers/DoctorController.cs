﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.NewFolder;
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
    }
}