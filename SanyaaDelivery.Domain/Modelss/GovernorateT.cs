using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class GovernorateT
    {
        public GovernorateT()
        {
            AddressT = new HashSet<AddressT>();
            CityT = new HashSet<CityT>();
        }

        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public string LocationLat { get; set; }
        public string LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? CountryId { get; set; }

        public CountryT Country { get; set; }
        public ICollection<AddressT> AddressT { get; set; }
        public ICollection<CityT> CityT { get; set; }
    }
}
