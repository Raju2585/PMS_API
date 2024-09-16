using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IReceptionistRepository
    {
        public Task<Receptionist> GetReceptionistByEmail(string email);
    }
}
