using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Cleaningsubscribers
{
    public int Id { get; set; }

    public int Package { get; set; }

    public DateTime SubscribeDate { get; set; }

    public int ClientId { get; set; }

    public int SystemUserId { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
