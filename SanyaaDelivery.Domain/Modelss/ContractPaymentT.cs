using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ContractPaymentT
    {
        public int ContractPaymentId { get; set; }
        public int ContactId { get; set; }
        public int PaymentAmount { get; set; }
        public DateTime? CreateTime { get; set; }
        public int SystemUserId { get; set; }

        public SiteContractT Contact { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
