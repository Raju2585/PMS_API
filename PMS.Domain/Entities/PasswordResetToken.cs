using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities
{
    public class PasswordResetToken
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        // Navigation property 
        public virtual Patient Patient { get; set; }
    }
}
