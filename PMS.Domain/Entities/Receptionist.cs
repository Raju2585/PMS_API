using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities
{
    public class Receptionist: ApplicationUser
    {
        public string ReceptionistName { get; set; }
        public int HospitalId { get; set; }

        public virtual Hospital? Hospital { get; set; }
    }

}
