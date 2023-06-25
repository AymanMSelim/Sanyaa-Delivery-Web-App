using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class GovernorateT
{
    public int GovernorateId { get; set; }

    public string GovernorateName { get; set; }

    public string LocationLat { get; set; }

    public string LocationLang { get; set; }

    public string LocationUrl { get; set; }

    public int? CountryId { get; set; }

    public virtual ICollection<AddressT> AddressT { get; set; } = new List<AddressT>();

    public virtual ICollection<CityT> CityT { get; set; } = new List<CityT>();

    public virtual CountryT Country { get; set; }
}
