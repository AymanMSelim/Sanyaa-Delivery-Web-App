using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestDelayedT
    {
        public int RequestId { get; set; }
        public DateTime DelayRequestTimestamp { get; set; }
        public string DelayRequestReason { get; set; }
        public DateTime DelayRequestNewTimestamp { get; set; }
        public int SystemUserId { get; set; }

        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
