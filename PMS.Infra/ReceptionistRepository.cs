using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
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
        public async Task<Receptionist> GetReceptionistByEmail(string email)
        {
            var receptionist=await _context.Receptionists.FirstOrDefaultAsync(r=>r.Email==email);
            return receptionist;
        }
    }
}
