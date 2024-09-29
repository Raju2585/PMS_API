using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class RegisterDto
    {
        // Common properties
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Patient-specific properties
        public string? PatientName { get; set; }
        public string? ContactNumber { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public DateTime? Date { get; set; }

        // Receptionist-specific properties
        public string? ReceptionistName { get; set; }
        public int? HospitalId { get; set; }
    }
}
