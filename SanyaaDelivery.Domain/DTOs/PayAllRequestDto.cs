using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class PayAllRequestDto
    {
        public string EmployeeId { get; set; }
        public int RequestCount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<PayRequestDto> RequestList { get; set; }
    }
}
