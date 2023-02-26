﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class ClientSubscriptionT
    {
        public int ClientSubscriptionId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public int SubscriptionServiceId { get; set; }
        public int? AddressId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? VisitTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public int SystemUserId { get; set; }
        public bool IsCanceled { get; set; }

        public AddressT Address { get; set; }
        public ClientT Client { get; set; }
        public EmployeeT Employee { get; set; }
        public SubscriptionT Subscription { get; set; }
        public SubscriptionServiceT SubscriptionService { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
