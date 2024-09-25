using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Response
{
    public class AppointmentRes
    {
        public bool IsSuccess { get; set; }
        public Doctor_Slots? Doctor_Slots { get; set; }
        public string? Error { get; set; }
    }
}
