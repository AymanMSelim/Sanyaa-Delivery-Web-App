using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AddressAreaT
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public double? AreaLat { get; set; }
        public double? AreaLang { get; set; }
        public string AreaMapLocationText { get; set; }
        public int CityId { get; set; }
    }
}
