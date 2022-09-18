using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class EmployeeSubscriptionT
    {
        public EmployeeSubscriptionT()
        {
            EmployeeT = new HashSet<EmployeeT>();
        }

        public int SubscriptionId { get; set; }
        public string Description { get; set; }
        public int? InsuranceAmount { get; set; }
        public int? MaxRequestPrice { get; set; }
        public int? MaxRequestCount { get; set; }

        public ICollection<EmployeeT> EmployeeT { get; set; }
    }
}
