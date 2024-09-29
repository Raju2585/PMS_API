using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public object User { get; set; } 
        public string Token { get; set; }
    }

}
