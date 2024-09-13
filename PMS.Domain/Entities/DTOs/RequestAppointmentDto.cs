using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class RequestAppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusId { get; set; } = -1;
        public string HospitalName { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
