using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeNotPaidRequestDto
    {
        public int RequestId { get; set; }
        public string FinishTime { get; set; }
        public string DueTime { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public decimal EmployeePercentage { get; set; }
        public decimal CompanyPercentage { get; set; }
        public decimal InsuranceAmount { get; set; }
    }
}
