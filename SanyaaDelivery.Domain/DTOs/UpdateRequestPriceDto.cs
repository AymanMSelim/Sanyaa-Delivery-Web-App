using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UpdateRequestPriceDto
    {
        public int RequestId { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal EmployeePercentage { get; set; }
        public decimal CompanyPercentage { get; set; }
    }
}
