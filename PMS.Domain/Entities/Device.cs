using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int PatientId { get; set; }  

        // Navigation property
        public virtual Patient Patient { get; set; }
        public VitalSign VitalSign { get; set; }
    }

}
