using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class BroadcastRequestT
    {
        public int BroadcastRequestId { get; set; }
        public int RequestId { get; set; }
        public string EmployeeId { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ActionTime { get; set; }
        public bool IsListed { get; set; }
        public bool IsSeen { get; set; }

        public EmployeeT Employee { get; set; }
        public RequestT Request { get; set; }
    }
}
