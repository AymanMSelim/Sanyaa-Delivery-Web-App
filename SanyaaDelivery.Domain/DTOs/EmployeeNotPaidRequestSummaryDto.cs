using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeNotPaidRequestSummaryDto
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public decimal IncreaseDiscountTotal { get; set; }
        public int TotalUnPaidRequestCount { get; set; }
        public decimal TotalUnPaidRequestCost { get; set; }
        public decimal TotalCompanyPercentage { get; set; }
        public decimal TotalEmployeePercentage { get; set; }
        public bool AccountState { get; set; }
    }
}
