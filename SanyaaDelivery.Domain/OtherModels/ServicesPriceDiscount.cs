using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class ServicesPriceDiscountSummary
    {
        public decimal ServiceRatio { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalRatioPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetPrice { get; set; }
        public int TotalPoints { get; set; }
    }
}
