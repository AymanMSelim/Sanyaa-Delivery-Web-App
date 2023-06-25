using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FavouriteServiceT
{
    public int FavouriteServiceId { get; set; }

    public int ServiceId { get; set; }

    public int ClientId { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual ServiceT Service { get; set; }
}
