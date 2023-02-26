using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RegionT
    {
        public RegionT()
        {
            AddressT = new HashSet<AddressT>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public short? DeliveryPrice { get; set; }
        public bool IsDeliveryPriceActive { get; set; }
        public short? MinimumCharge { get; set; }
        public bool IsMinimumChargeActive { get; set; }
        public string LocationLat { get; set; }
        public string LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? CityId { get; set; }

        public CityT City { get; set; }
        public ICollection<AddressT> AddressT { get; set; }
    }
}
