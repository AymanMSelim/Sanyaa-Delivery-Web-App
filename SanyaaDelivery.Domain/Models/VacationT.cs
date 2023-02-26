using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class VacationT
    {
        public int VacationId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Day { get; set; }
        public DateTime CreationTime { get; set; }
        public int SystemUserId { get; set; }

        public EmployeeT Employee { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
