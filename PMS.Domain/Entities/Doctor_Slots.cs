using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities
{
    public class Doctor_Slots
    {
        [Key]
        public int SlotId { get; set; }
        public int DoctorId { get; set; }
        public DateOnly date {  get; set; }
        public bool Slot_1 { get; set; }=false;
        public bool Slot_2 { get; set; } = false;
        public bool Slot_3 { get; set; } = false;
        public bool Slot_4 { get; set; } = false;
        public bool Slot_5 { get; set; } = false;
        public virtual Doctor Doctor { get; set; }
    }
}
