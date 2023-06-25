using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestDiscountT
{
    public int RequestDiscountId { get; set; }

    public int RequestId { get; set; }

    public sbyte DiscountTypeId { get; set; }

    public string Description { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal CompanyPercentage { get; set; }

    public DateTime CreationTime { get; set; }

    public int SystemUserId { get; set; }

    public virtual DiscountTypeT DiscountType { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
