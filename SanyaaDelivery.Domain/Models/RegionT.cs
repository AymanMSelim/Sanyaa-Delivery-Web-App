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
        public decimal? LocationLat { get; set; }
        public decimal? LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? CityId { get; set; }

        public CityT City { get; set; }
        public ICollection<AddressT> AddressT { get; set; }
    }
}
