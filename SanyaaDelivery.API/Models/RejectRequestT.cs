﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class RejectRequestT
    {
        public int RejectRequestId { get; set; }
        public string EmployeeId { get; set; }
        public int RequestId { get; set; }
        public DateTime? RejectRequestTimestamp { get; set; }

        public EmployeeT Employee { get; set; }
        public RequestT Request { get; set; }
    }
}
