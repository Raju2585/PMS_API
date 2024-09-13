using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public int StatusId { get; set; } = -1;
        public string HospitalName { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

}
