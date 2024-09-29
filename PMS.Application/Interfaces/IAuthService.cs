using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Authenticate(string email, string password);
    }
}
