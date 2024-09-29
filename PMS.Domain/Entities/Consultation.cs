using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Consultation
    {
        [Key]
        public int ConsultationId { get; set; }
        public string Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

}
