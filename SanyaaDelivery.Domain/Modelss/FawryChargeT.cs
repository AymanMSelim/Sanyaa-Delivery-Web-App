using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class FawryChargeT
    {
        public FawryChargeT()
        {
            FawryChargeRequestT = new HashSet<FawryChargeRequestT>();
        }

        public int SystemId { get; set; }
        public long? FawryRefNumber { get; set; }
        public string ChargeStatus { get; set; }
        public double? ChargeAmount { get; set; }
        public DateTime? ChargeExpireDate { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? RecordTimestamp { get; set; }
        public bool? IsConfirmed { get; set; }

        public EmployeeT Employee { get; set; }
        public ICollection<FawryChargeRequestT> FawryChargeRequestT { get; set; }
    }
}
