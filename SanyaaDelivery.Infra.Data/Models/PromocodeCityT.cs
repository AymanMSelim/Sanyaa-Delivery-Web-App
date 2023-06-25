using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PromocodeCityT
{
    public int PromocodeCityId { get; set; }

    public int? CityId { get; set; }

    public int? PromocodeId { get; set; }

    public virtual CityT City { get; set; }

    public virtual PromocodeT Promocode { get; set; }
}
