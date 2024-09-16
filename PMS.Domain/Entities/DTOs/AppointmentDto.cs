using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int height { get; set; }
        public int Weight { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusId { get; set; }
        public string HospitalName { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? DoctorName { get; set; } 
    }

}
