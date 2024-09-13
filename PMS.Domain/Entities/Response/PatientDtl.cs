using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Response
{
    public class PatientDtl
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string ContactNumber { get; set; }
        public string DeviceName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; } = new HashSet<MedicalHistory>();
        public virtual Device Device { get; set; }
    }
}
