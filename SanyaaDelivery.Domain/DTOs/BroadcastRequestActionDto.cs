using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class BroadcastRequestActionDto
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; }
        public string Status { get; set; }
    }
}
