using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SubscriptionSequenceT
    {
        public int ClientSubscriptionSequenceId { get; set; }
        public int SubscriptionId { get; set; }
        public sbyte Sequence { get; set; }
        public decimal DiscountPercentage { get; set; }

        public SubscriptionT Subscription { get; set; }
    }
}
