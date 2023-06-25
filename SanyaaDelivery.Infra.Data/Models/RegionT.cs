using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RegionT
{
    public int RegionId { get; set; }

    public string RegionName { get; set; }

    public short? DeliveryPrice { get; set; }

    public ulong IsDeliveryPriceActive { get; set; }

    public short? MinimumCharge { get; set; }

    public ulong IsMinimumChargeActive { get; set; }

    public string LocationLat { get; set; }

    public string LocationLang { get; set; }

    public string LocationUrl { get; set; }

    public int? CityId { get; set; }

    public virtual ICollection<AddressT> AddressT { get; set; } = new List<AddressT>();

    public virtual CityT City { get; set; }
}
