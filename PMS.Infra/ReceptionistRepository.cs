using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class ReceptionistRepository:IReceptionistRepository
    {
        private readonly IApplicationDbContext _context;
        public ReceptionistRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReceptionistDtl> GetReceptionistByEmail(string email)
        {
            var receptionist = await _context.Receptionists
                .Where(r => r.Email == email)
                .Include(d => d.Hospital)
                .Select(a => new ReceptionistDtl
                {
                    ReceptionistId = a.ReceptionistId,
                    Email = a.Email,
                    HospitalId = a.HospitalId,
                    Password=a.Password,
                    ReceptionistName=a.ReceptionistName,
                    HospitalName = a.Hospital.HospitalName
                }).FirstOrDefaultAsync();
                
            return receptionist;
        }
    }
}
