using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.NewFolder
{
    public class DoctorDTO
    {
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsAvailable { get; set; }
        public byte[] Image { get; set; }
    }
}
