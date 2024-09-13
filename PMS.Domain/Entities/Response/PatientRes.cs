using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Response
{
    public class PatientRes
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }=null;
        public string? PatientEmail { get; set; } = null;

    }
}
