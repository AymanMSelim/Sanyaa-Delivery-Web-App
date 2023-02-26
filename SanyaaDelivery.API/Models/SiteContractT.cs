using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class SiteContractT
    {
        public int ContractId { get; set; }
        public string ContractDesception { get; set; }
        public int SiteId { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainAmount { get; set; }
        public DateTime? CreationTime { get; set; }
        public int SystemUserId { get; set; }
        public DateTime? ModificationTime { get; set; }
        public int? ModificationSystemUserId { get; set; }

        public SystemUserT ModificationSystemUser { get; set; }
        public SiteT Site { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
