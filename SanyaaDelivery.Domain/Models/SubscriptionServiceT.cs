using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SubscriptionServiceT
    {
        public SubscriptionServiceT()
        {
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            SubscriptionSequenceT = new HashSet<SubscriptionSequenceT>();
        }

        public int SubscriptionServiceId { get; set; }
        public int SubscriptionId { get; set; }
        public int ServiceId { get; set; }
        public decimal TotalPricePerMonth { get; set; }
        public decimal Discount { get; set; }
        public string Info { get; set; }

        public ServiceT Service { get; set; }
        public SubscriptionT Subscription { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<SubscriptionSequenceT> SubscriptionSequenceT { get; set; }
    }
}
