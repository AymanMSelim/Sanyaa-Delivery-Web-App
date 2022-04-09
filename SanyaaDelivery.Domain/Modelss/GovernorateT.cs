using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class GovernorateT
    {
        public GovernorateT()
        {
            CityT = new HashSet<CityT>();
        }

        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public decimal? LocationLat { get; set; }
        public decimal? LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? CountryId { get; set; }

        public CountryT Country { get; set; }
        public ICollection<CityT> CityT { get; set; }
    }
}
