using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class GenerateRefNumberForRequestDto
    {
        public string EmployeeId { get; set; }
        public List<int> RequestList { get; set; }
    }
}
