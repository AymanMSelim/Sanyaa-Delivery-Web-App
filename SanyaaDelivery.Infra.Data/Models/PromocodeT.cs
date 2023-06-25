using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

/// <summary>
/// 				
/// </summary>
public partial class PromocodeT
{
    public int PromocodeId { get; set; }

    public string Promocode { get; set; }

    public string Description { get; set; }

    public int Type { get; set; }

    public int Value { get; set; }

    public int MinimumCharge { get; set; }

    public int MaxUsageCount { get; set; }

    public DateTime ExpireTime { get; set; }

    public int UsageCount { get; set; }

    public decimal CompanyDiscountPercentage { get; set; }

    public ulong AutoApply { get; set; }

    public int SystemUserId { get; set; }

    public virtual ICollection<CartT> CartT { get; set; } = new List<CartT>();

    public virtual ICollection<PromocodeCityT> PromocodeCityT { get; set; } = new List<PromocodeCityT>();

    public virtual ICollection<PromocodeDepartmentT> PromocodeDepartmentT { get; set; } = new List<PromocodeDepartmentT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual SystemUserT SystemUser { get; set; }
}
