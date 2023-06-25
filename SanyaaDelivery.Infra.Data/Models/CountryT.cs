using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class CountryT
{
    public int CountryId { get; set; }

    public string CountryName { get; set; }

    public string LocationLat { get; set; }

    public string LocationLang { get; set; }

    public string LocationUrl { get; set; }

    public virtual ICollection<GovernorateT> GovernorateT { get; set; } = new List<GovernorateT>();
}
