using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeNotPaidRequestDto
    {
        public int RequestId { get; set; }
        public DateTime FinishTime { get; set; }
        public DateTime DueTime { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public decimal EmployeePercentage { get; set; }
        public decimal CompanyPercentage { get; set; }
    }
}
