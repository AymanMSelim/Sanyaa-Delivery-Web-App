using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SiteRequestT
    {
        public int SiteRequestId { get; set; }
        public int SiteId { get; set; }
        public int RequestId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? CreationTime { get; set; }

        public RequestT Request { get; set; }
        public SiteT Site { get; set; }
    }
}
