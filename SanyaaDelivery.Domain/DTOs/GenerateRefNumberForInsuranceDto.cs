using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class GenerateRefNumberForInsuranceDto
    {
        public string EmployeeId { get; set; }
        public decimal Amount { get; set; }
    }
}
