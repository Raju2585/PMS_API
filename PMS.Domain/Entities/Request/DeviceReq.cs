using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Request
{
    public class DeviceReq
    {
        public int PatientId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
