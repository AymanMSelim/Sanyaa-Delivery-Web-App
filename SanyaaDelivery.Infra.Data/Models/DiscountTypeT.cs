using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DiscountTypeT
{
    public sbyte DiscountTypeId { get; set; }

    public string DiscountTypeName { get; set; }

    public string DiscountTypeDes { get; set; }

    public ulong? IsActive { get; set; }

    public virtual ICollection<RequestDiscountT> RequestDiscountT { get; set; } = new List<RequestDiscountT>();
}
