using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class PromocodeCityT
    {
        public int PromocodeCityId { get; set; }
        public int? CityId { get; set; }
        public int? PromocodeId { get; set; }

        public CityT City { get; set; }
        public PromocodeT Promocode { get; set; }
    }
}
