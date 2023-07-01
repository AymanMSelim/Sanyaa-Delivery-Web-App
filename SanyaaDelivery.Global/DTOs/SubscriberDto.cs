using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.DTOs
{
    public class SubscriberDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int? ClientSibscriptionId { get; set; }
        public int? SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string ServiceName { get; set; }
        public int? ServiceId { get; set; }
        public int? SubscriptionServiceId { get; set; }
        public int? AddressId { get; set; }
        public int? PhoneId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsContract { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? ClientBranchId { get; set; }
        public bool IsCanceled { get; set; }
    }
}
