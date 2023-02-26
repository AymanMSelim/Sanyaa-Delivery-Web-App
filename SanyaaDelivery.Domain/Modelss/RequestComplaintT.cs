using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestComplaintT
    {
        public int RequestId { get; set; }
        public DateTime ComplaintTimestamp { get; set; }
        public string ComplaintDes { get; set; }
        public int? NewRequestId { get; set; }
        public string ComplaintIsSolved { get; set; }
        public bool? IsSolved { get; set; }
        public int SystemUserId { get; set; }

        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
