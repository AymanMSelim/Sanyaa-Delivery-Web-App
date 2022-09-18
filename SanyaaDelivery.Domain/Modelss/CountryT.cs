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
        public string LocationLat { get; set; }
        public string LocationLang { get; set; }
        public string LocationUrl { get; set; }

        public ICollection<GovernorateT> GovernorateT { get; set; }
    }
}
