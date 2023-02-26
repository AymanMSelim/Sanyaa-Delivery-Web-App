using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class EmployeeReviewT
    {
        public int EmployeeReviewId { get; set; }
        public string EmployeeId { get; set; }
        public int ClientId { get; set; }
        public int? RequestId { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        public DateTime CreationTime { get; set; }

        public ClientT Client { get; set; }
        public EmployeeT Employee { get; set; }
        public RequestT Request { get; set; }
    }
}
