using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class InsurancePaymentT
    {
        public int InsurancePaymentId { get; set; }
        public string EmployeeId { get; set; }
        public int? ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? SystemUserId { get; set; }

        public EmployeeT Employee { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
