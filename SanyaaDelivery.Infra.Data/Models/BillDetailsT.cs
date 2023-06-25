using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class BillDetailsT
{
    public string BillNumber { get; set; }

    public sbyte BillType { get; set; }

    public float BillCost { get; set; }

    public string BillIo { get; set; }

    public string BillNote { get; set; }

    public virtual BillNumberT BillNumberNavigation { get; set; }
}
