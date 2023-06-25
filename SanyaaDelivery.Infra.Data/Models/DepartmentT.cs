using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DepartmentT
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }

    public sbyte? DepartmentPercentage { get; set; }

    public string DepartmentDes { get; set; }

    public string DepartmentImage { get; set; }

    public sbyte? MaximumDiscountPercentage { get; set; }

    public string Terms { get; set; }

    public ulong IncludeDeliveryPrice { get; set; }

    public virtual ICollection<CartT> CartT { get; set; } = new List<CartT>();

    public virtual ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; } = new List<DepartmentEmployeeT>();

    public virtual ICollection<DepartmentSub0T> DepartmentSub0T { get; set; } = new List<DepartmentSub0T>();

    public virtual ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; } = new List<LandingScreenItemDetailsT>();

    public virtual ICollection<OpeningSoonDepartmentT> OpeningSoonDepartmentT { get; set; } = new List<OpeningSoonDepartmentT>();

    public virtual ICollection<PromocodeDepartmentT> PromocodeDepartmentT { get; set; } = new List<PromocodeDepartmentT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; } = new List<ServiceRatioDetailsT>();

    public virtual ICollection<SubscriptionT> SubscriptionT { get; set; } = new List<SubscriptionT>();
}
