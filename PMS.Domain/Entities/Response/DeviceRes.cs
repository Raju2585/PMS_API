using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Response
{
    public class DeviceRes
    {
        public bool IsSuccess { get; set; }
        public Device Device { get; set; }
        public VitalSign VitalSign { get; set; }
        public string? Error { get; set; }
        public bool IsDeviceExisted { get; set; }=false;
    }
}
