using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Notification_id { get; set; }
        public string Notification_Message { get; set; }
        public int Notifcation_Count { get; set; }
        public bool IsRead { get; set; } = false;
        public string Id { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
