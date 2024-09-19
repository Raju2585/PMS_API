using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class ReceptionistDtl
    {
        public int ReceptionistId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int HospitalId { get; set; }
        public string ReceptionistName { get; set; }
        public string HospitalName { get; set; }
    }
}
