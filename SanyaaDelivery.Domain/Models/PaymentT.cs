using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class PaymentT
    {
        public int RequestId { get; set; }
        public DateTime PaymentTimestamp { get; set; }
        public double Payment { get; set; }
        public int SystemUserId { get; set; }

        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
