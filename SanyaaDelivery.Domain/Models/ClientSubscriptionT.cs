using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ClientSubscriptionT
    {
        public int ClientSubscriptionId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int SystemUserId { get; set; }

        public ClientT Client { get; set; }
        public SubscriptionT Subscription { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
