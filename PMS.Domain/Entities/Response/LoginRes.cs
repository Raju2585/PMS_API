using PMS.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.Response
{
    public class LoginRes
    {
        public PatientReq Patient { get; set; }
        public bool IsLogged { get; set; } = false;
        public string Token { get; set; }
    }
}
