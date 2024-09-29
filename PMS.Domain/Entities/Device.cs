using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string Id { get; set; }  
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property
        public virtual Patient Patient { get; set; }
        public VitalSign VitalSign { get; set; }
    }

}
