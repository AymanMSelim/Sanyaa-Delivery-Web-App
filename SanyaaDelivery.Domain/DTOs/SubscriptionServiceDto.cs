using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SubscriptionServiceDto
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }
        public int SubscriptionServiceId { get; set; }
        public string Info { get; set; }
    }
}
