using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.NewFolder
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
        //public bool IsAvailable { get; set; }
        public byte[] Image { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        public string DoctorEmail { get; set; }
        public string ContactNumber { get; set; }
        public int HospitalId { get; set; }
    }
}