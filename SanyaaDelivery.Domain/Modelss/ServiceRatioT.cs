using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ServiceRatioT
    {
        public ServiceRatioT()
        {
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
        }

        public int ServiceRatioId { get; set; }
        public string Description { get; set; }
        public decimal? Ratio { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
    }
}
