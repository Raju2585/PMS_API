using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class MedicalHistoryDTOs
    {
        
        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordedDate { get; set; }
        public string Reason { get; set; }
    }
}
