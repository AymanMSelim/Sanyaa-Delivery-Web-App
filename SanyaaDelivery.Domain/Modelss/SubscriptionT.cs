using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SubscriptionT
    {
        public SubscriptionT()
        {
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            RequestT = new HashSet<RequestT>();
            SubscriptionServiceT = new HashSet<SubscriptionServiceT>();
        }

        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public sbyte? RequestNumberPerMonth { get; set; }
        public decimal? StartFromPrice { get; set; }
        public string Condition { get; set; }
        public bool? IsActive { get; set; }

        public DepartmentT Department { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SubscriptionServiceT> SubscriptionServiceT { get; set; }
    }
}
