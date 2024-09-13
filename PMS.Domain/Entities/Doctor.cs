using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsAvailable { get; set; }=true;
        public int HospitalId { get; set; }
        public byte[]? Image {  get; set; }=null;
        //public int? AppointmentId { get; set; }=null ;

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
        public virtual Hospital Hospital { get; set; }
    }
                                                                                                                                                                                      
}
