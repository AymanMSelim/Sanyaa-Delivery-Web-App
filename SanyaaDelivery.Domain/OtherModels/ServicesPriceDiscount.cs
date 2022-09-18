using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class ServicesPriceDiscountSummary
    {
        public int ServiceRatio { get; set; }
        public int TotalPrice { get; set; }
        public int TotalRatioPrice { get; set; }
        public int TotalDiscount { get; set; }
        public int NetPrice { get; set; }
        public int TotalPoints { get; set; }
    }
}
