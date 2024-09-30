using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.Entities.DTOs;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Application.Interfaces;

namespace PMS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHospitalService _hospitalService;
        public AuthController(
            IAuthService authService,
            UserManager<ApplicationUser> userManager,
            IHospitalService hospitalService)
        {
            _authService = authService;
            _userManager = userManager;
            _hospitalService = hospitalService;
        }
        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientReq patientReq)
        {
            var user = new Patient
            {
                UserName = patientReq.Email,
                Email = patientReq.Email,
                PatientName = patientReq.FirstName + " " + patientReq.LastName,
                ContactNumber = patientReq.ContactNumber,
                Age = patientReq.Age,
                Gender = patientReq.Gender,
                Role = "PATIENT",
                Date=DateTime.Now
                
            };

            var result = await _userManager.CreateAsync(user, patientReq.Password);
            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto { IsSuccess = true, User = user });
            }
            return BadRequest(new AuthResponseDto { IsSuccess = false, Error = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }

        [HttpPost("register/receptionist")]
        public async Task<IActionResult> RegisterReceptionist([FromBody] ReceptionistReq receptionistReq)
        {
            var user = new Receptionist
            {
                UserName = receptionistReq.Email,
                Role = "RECEPTIONIST",
                Email = receptionistReq.Email,
                ReceptionistName = receptionistReq.ReceptionistName,
                HospitalId = receptionistReq.HospitalId
            };

            var result = await _userManager.CreateAsync(user, receptionistReq.Password);
            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto { IsSuccess = true, User = user });
            }
            return BadRequest(new AuthResponseDto { IsSuccess = false, Error = string.Join(", ", result.Errors.Select(e => e.Description)) });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq loginReq)
        {
            var response = await _authService.Authenticate(loginReq.Email, loginReq.Password);

            if (!response.IsSuccess)
                return Unauthorized(response);

            // Fetch user details for response
            var user = await _userManager.FindByEmailAsync(loginReq.Email);

            if (user is Patient patient)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    User = new
                    {
                        Id=user.Id,
                        Token = response.Token,
                        Role = patient.Role,
                        Email = patient.Email,
                        PatientName = patient.PatientName,
                        ContactNumber = patient.ContactNumber,
                        Age = patient.Age,
                        Gender = patient.Gender,
                        Date = patient.Date
                    }
                });
            }
            else if (user is Receptionist receptionist)
            {
                var hospital = await _hospitalService.GetHospitalById(receptionist.HospitalId);
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    User = new
                    {
                        Token = response.Token,
                        Role = receptionist.Role,
                        Email = receptionist.Email,
                        ReceptionistName = receptionist.ReceptionistName,
                        HospitalName = hospital.HospitalName
                    }
                });
            }

            return Unauthorized(new AuthResponseDto
            {
                IsSuccess = false,
                Error = "User not found."
            });
        }

    }
}
