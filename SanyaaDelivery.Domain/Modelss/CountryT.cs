using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CountryT
    {
        public CountryT()
        {
            GovernorateT = new HashSet<GovernorateT>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public decimal? LocationLat { get; set; }
        public decimal? LocationLang { get; set; }
        public string LocationUrl { get; set; }

        public ICollection<GovernorateT> GovernorateT { get; set; }
    }
}
