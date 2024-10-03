using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities
{
    public class ResetPasswordReq
    {
        public string Email { get; set; }
        public string Token {  get; set; }
        public string NewPassword {  get; set; }
    }
}
