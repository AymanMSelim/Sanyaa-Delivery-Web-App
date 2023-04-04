using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class OpreationT
    {
        public string EmployeeId { get; set; }
        public bool IsActive { get; set; }
        public bool OpenVacation { get; set; }
        public DateTime LastActiveTime { get; set; }
        public int? PreferredWorkingStartHour { get; set; }
        public int? PreferredWorkingEndHour { get; set; }

        public EmployeeT Employee { get; set; }
    }
}
