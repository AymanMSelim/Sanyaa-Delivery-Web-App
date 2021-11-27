using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AddressCityT
    {
        public AddressCityT()
        {
            ServiceCityRatioT = new HashSet<ServiceCityRatioT>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int GovId { get; set; }

        public AddressGovT Gov { get; set; }
        public ICollection<ServiceCityRatioT> ServiceCityRatioT { get; set; }
    }
}
