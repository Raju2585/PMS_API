using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IForgetPasswordService
    {
        Task<bool> SendResetLinkAsync(string email);
        Task<bool> ResetPasswordAsync(string email,string token,string password);
        Task<bool> ValidateTokenAsync(string email, string token);
    }
}
