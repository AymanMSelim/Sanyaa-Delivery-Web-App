using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AddRequestDto
    {
        public int? ClientId { get; set; }
        public int? AddressId { get; set; }
        public int? PhoneId { get; set; }
        public int? SiteId { get; set; }
        public int? ClientSubscriptionId { get; set; }
        public DateTime? RequestTime { get; set; }
        public string EmployeeId { get; set; }
        public bool SkipCheckEmployeeStatus { get; set; }
        public decimal AdditionalDiscount { get; set; }
        public decimal AdditionalDiscountCompanyPercantage { get; set; }
    }
}
