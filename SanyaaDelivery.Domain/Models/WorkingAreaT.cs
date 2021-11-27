using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class WorkingAreaT
    {
        public WorkingAreaT()
        {
            DeliveryPriceT = new HashSet<DeliveryPriceT>();
        }

        public string WorkingAreaGov { get; set; }
        public string WorkingAreaCity { get; set; }
        public string WorkingAreaRegion { get; set; }
        public int BranchId { get; set; }
        public int WorkingAreaId { get; set; }

        public BranchT Branch { get; set; }
        public ICollection<DeliveryPriceT> DeliveryPriceT { get; set; }
    }
}
