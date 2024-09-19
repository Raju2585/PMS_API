using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReceptionistName { get; set; }
        public int HospitalId { get; set; }
        public string Password { get; set; }


        public virtual Hospital? Hospital { get; set; }
    }

}
