using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Request
{
    public class PatientReq
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string DeviceName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
