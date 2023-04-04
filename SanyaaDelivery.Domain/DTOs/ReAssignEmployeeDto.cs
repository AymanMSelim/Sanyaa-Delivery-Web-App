using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ReAssignEmployeeDto
    {
        public string EmployeeId { get; set; }
        public int RequestId { get; set; }
        public DateTime? RequestTime { get; set; }
        public bool SkipCheckEmployee { get; set; }
    }
}
