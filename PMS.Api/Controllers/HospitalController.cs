using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }
        [HttpGet]
        [Route("Get/All/Hospitals")]
        public async Task<ActionResult<List<Hospital>>> GetHospitals()
        {
            try
            {
                var hospitals = await _hospitalService.GetAllHospitals();
                return Ok(hospitals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error in filteringHospitals: " + ex.Message);
            }

        }
        [HttpGet]
        [Route("Get/HospitalsByLocation")]
        public async Task<ActionResult<List<Hospital>>> GetHospitalByLocation(string location)
        {
            try
            {
                var hospitalsList = await _hospitalService.GetHospitalByLocation(location);
                return Ok(hospitalsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error in filteringHospitals: " + ex.Message);
            }

        }
        [HttpGet]
        [Route("HospitalsByPincode")]
        public async Task<ActionResult<List<Hospital>>> GetHospitalByPincode(int pincode)
        {
            try
            {
                var hospitalListByPinCode = await _hospitalService.GetHospitalByPinCode(pincode);
                return Ok(hospitalListByPinCode);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
