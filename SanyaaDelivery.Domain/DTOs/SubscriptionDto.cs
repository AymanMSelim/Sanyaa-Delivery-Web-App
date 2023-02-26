using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public sbyte? RequestNumberPerMonth { get; set; }
        public decimal? StartFromPrice { get; set; }
        public string Condition { get; set; }
        public bool? IsActive { get; set; }
        public string DepartmentName { get; set; }
        public string PriceCaption { get; set; }
        public string StartFromPriceC { get; set; }
        public List<string> Specification { get; set; }
    }
}
