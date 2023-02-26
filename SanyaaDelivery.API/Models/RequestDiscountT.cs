using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class RequestDiscountT
    {
        public int RequestDiscountId { get; set; }
        public int RequestId { get; set; }
        public sbyte DiscountTypeId { get; set; }
        public string Description { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal CompanyPercentage { get; set; }
        public DateTime CreationTime { get; set; }
        public int SystemUserId { get; set; }

        public DiscountTypeT DiscountType { get; set; }
        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
