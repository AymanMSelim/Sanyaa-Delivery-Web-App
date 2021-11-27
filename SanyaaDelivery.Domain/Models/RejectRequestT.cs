using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RejectRequestT
    {
        public string EmployeeId { get; set; }
        public int? RequestId { get; set; }
        public DateTime? RejectRequestTimestamp { get; set; }
        public int RejectRequestId { get; set; }

        public EmployeeT Employee { get; set; }
        public RequestT Request { get; set; }
    }
}
