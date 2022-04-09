using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SiteContractT
    {
        public SiteContractT()
        {
            ContractPaymentT = new HashSet<ContractPaymentT>();
        }

        public int SiteContractId { get; set; }
        public string ContractDesception { get; set; }
        public int SiteId { get; set; }
        public int? ContractCost { get; set; }
        public int? AmountPaid { get; set; }
        public int? RemainAmount { get; set; }
        public DateTime? CreateTime { get; set; }
        public int SystemUserId { get; set; }

        public SiteT Site { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<ContractPaymentT> ContractPaymentT { get; set; }
    }
}
