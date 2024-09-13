using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class MedicalHistory
    {
        [Key]
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordedDate { get; set; }
        public string Reason { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
    }

}
