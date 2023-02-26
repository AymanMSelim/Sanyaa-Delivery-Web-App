using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class PartinerPaymentT
    {
        public PartinerPaymentT()
        {
            PartinerPaymentRequestT = new HashSet<PartinerPaymentRequestT>();
        }

        public int Id { get; set; }
        public DateTime? RecordTimestamp { get; set; }
        public int SystemUserId { get; set; }
        public double? Amount { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public SystemUserT SystemUser { get; set; }
        public ICollection<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; }
    }
}
