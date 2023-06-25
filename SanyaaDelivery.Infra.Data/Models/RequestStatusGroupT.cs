using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestStatusGroupT
{
    public int RequestStatusGroupId { get; set; }

    public string GroupName { get; set; }

    public string Description { get; set; }

    public virtual ICollection<RequestStatusT> RequestStatusT { get; set; } = new List<RequestStatusT>();
}
