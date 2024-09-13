using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class VitalSign
    {
        [Key]
        public int VitalSignId { get; set; }
        public int DeviceId { get; set; }  
        public int HeartRate { get; set; }
        public int OxygenSaturation { get; set; }
        public string BloodPressure { get; set; }
        public float Temperature { get; set; }
        public int RespiratoryRate { get; set; }

        // Navigation property
        public virtual Device Device { get; set; }
    }

}
