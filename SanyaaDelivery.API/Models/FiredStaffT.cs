using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class FiredStaffT
    {
        public string EmployeeId { get; set; }
        public DateTime FiredDate { get; set; }
        public string FiredReasons { get; set; }

        public EmployeeT Employee { get; set; }
    }
}
