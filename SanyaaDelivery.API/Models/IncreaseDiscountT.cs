using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class IncreaseDiscountT
    {
        public string EmployeeId { get; set; }
        public DateTime Timestamp { get; set; }
        public sbyte IncreaseDiscountType { get; set; }
        public short IncreaseDiscountValue { get; set; }
        public string IncreaseDiscountReason { get; set; }
        public int SystemUserId { get; set; }

        public EmployeeT Employee { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
