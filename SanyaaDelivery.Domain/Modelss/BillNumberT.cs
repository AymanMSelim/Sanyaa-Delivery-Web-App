using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class BillNumberT
    {
        public BillNumberT()
        {
            BillDetailsT = new HashSet<BillDetailsT>();
        }

        public string BillNumber { get; set; }
        public int RequestId { get; set; }
        public DateTime? BillTimestamp { get; set; }
        public int SystemUserId { get; set; }

        public RequestT Request { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<BillDetailsT> BillDetailsT { get; set; }
    }
}
