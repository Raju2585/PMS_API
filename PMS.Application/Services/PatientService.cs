using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Request;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class PatientService : IPatientService, ILoginService<PatientDtl>
    {
        private readonly IPatientRepository _repository;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        public PatientService(
            IPatientRepository repository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _repository = repository;
            _config = configuration;
            _mapper = mapper;
        }

        public async Task<PatientDtl> GetPatientById(string patientId)
        {
            try
            {
                var patient = await _repository.GetPatientById(patientId);
                return patient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<PatientDtl> GetPatientByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email cannot be null or empty.", nameof(email));
            }
            try
            {
                var patient = await _repository.GetPatientEmail(email);
                if (patient == null)
                {
                    return null;
                }
                return _mapper.Map<PatientDtl>(patient);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error fetching patient by email: {ex.Message}");
                return null;
            }


        }
        public async Task<string> GenerateToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, email)
    };

            var token = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UpdatePatientPassword(string patientEmail, string newPassword)
        {
            try
            {
                return await _repository.UpdatePatientPassword(patientEmail,newPassword);
            }
            catch
            {
                return false;
            }
        }
    }
}
