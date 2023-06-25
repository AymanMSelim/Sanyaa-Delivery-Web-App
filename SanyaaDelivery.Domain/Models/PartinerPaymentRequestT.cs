using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class PartinerPaymentRequestT
    {
        public int PaymentId { get; set; }
        public int RequestId { get; set; }

        public PartinerPaymentT Payment { get; set; }
        public RequestT Request { get; set; }
        public int Id { get; set; }
    }
}
