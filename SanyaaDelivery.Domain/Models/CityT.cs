using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CityT
    {
        public CityT()
        {
            RegionT = new HashSet<RegionT>();
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public decimal? LoactionLat { get; set; }
        public decimal? LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? GovernorateId { get; set; }

        public GovernorateT Governorate { get; set; }
        public ICollection<RegionT> RegionT { get; set; }
        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
    }
}
