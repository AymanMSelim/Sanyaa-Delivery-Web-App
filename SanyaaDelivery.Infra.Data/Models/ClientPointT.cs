using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ClientPointT
{
    public int ClientPointId { get; set; }

    public int ClientId { get; set; }

    public int? Points { get; set; }

    public sbyte? PointType { get; set; }

    public string Reason { get; set; }

    public int? RequestId { get; set; }

    public DateTime? CreationDate { get; set; }

    public int SystemUserId { get; set; }

    public virtual ClientT Client { get; set; }
}
