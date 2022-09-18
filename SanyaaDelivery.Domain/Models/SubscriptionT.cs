using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SubscriptionT
    {
        public SubscriptionT()
        {
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            SubscriptionSequenceT = new HashSet<SubscriptionSequenceT>();
        }

        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Description { get; set; }
        public int? DepartmentId { get; set; }
        public sbyte? RequestNumberPerMonth { get; set; }
        public bool? IsActive { get; set; }

        public DepartmentT Department { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<SubscriptionSequenceT> SubscriptionSequenceT { get; set; }
    }
}
