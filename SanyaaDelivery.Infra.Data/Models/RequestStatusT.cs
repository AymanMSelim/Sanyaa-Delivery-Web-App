using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestStatusT
{
    public sbyte RequestStatusId { get; set; }

    public string RequestStatusName { get; set; }

    public string RequestStatusDes { get; set; }

    public int? RequestStatusGroupId { get; set; }

    public virtual RequestStatusGroupT RequestStatusGroup { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();
}
