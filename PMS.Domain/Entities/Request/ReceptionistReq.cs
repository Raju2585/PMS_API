using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Request
{
    public class ReceptionistReq
    {
        public string Email { get; set; }
        public string ReceptionistName { get; set; }
        public int HospitalId { get; set; }
        public string Password { get; set; }
    }
}
