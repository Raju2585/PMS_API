using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public byte[]? HospitalImage { get; set; }=null;
        // Navigation property
        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }

}
