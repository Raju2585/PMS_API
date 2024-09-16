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
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class ReceptionistService:IReceptionistService,ILoginService<Receptionist>
    {
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IConfiguration _config;
        public ReceptionistService(IReceptionistRepository receptionistRepository, IConfiguration config)
        {
            _receptionistRepository = receptionistRepository;
            _config=config;
        }
        public async Task<Receptionist> GetReceptionistByEmail(string email)
        {
            try
            {
                var receptionist = await _receptionistRepository.GetReceptionistByEmail(email);
                if (receptionist != null)
                {
                    return receptionist;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        public async Task<LoginRes<Receptionist>> AuthenticateUser(LoginReq user)
        {
            var LoginRes = new LoginRes<Receptionist>();
            try
            {
                var receptionistOb = await _receptionistRepository.GetReceptionistByEmail(user.Email);
                if (receptionistOb != null && (user.Email == receptionistOb.Email && user.Password == receptionistOb.Password))
                {
                    LoginRes.User = receptionistOb;
                    LoginRes.IsLogged = true;
                }
            }
            catch (Exception ex)
            {
                return LoginRes;
            }

            return LoginRes;

        }
        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<LoginRes<Receptionist>> Login(LoginReq patient)
        {
            LoginRes<Receptionist> _user = null;
            try
            {
                _user = await AuthenticateUser(patient);
                if (_user != null && _user.IsLogged)
                {
                    _user.Token = await GenerateToken();
                }
            }
            catch (Exception e)
            {
                return _user;
            }
            return _user;
        }
    }
}
