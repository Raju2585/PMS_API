using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.Entities
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistId { get; set; }
        public string Name { get; set; }
        public string HospitalName { get; set; }
        public string Password { get; set; }

    }

}
