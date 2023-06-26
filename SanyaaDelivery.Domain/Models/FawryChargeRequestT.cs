using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class FawryChargeRequestT
    {
        public int Id { get; set; }
        public int ChargeId { get; set; }
        public int? RequestId { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public FawryChargeT Charge { get; set; }
        public RequestT Request { get; set; }
    }
}
