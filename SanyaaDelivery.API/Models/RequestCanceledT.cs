using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class RequestCanceledT
    {
        public int RequestId { get; set; }
        public DateTime CancelRequestTimestamp { get; set; }
        public string CancelRequestReason { get; set; }
        public int SystemUserId { get; set; }

        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
