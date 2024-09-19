using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface IReceptionistRepository
    {
        public Task<ReceptionistDtl> GetReceptionistByEmail(string email);
    }
}
