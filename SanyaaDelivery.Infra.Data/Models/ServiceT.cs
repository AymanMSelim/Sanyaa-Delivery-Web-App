using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ServiceT
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; }

    public int DepartmentId { get; set; }

    public decimal ServiceCost { get; set; }

    public decimal ServiceDuration { get; set; }

    public string ServiceDes { get; set; }

    public decimal? MaterialCost { get; set; }

    public int? ServicePoints { get; set; }

    public ulong NoDiscount { get; set; }

    public decimal? ServiceDiscount { get; set; }

    public int? DiscountServiceCount { get; set; }

    public decimal? CompanyDiscountPercentage { get; set; }

    public ulong NoMinimumCharge { get; set; }

    public ulong NoPointDiscount { get; set; }

    public ulong NoPromocodeDiscount { get; set; }

    public virtual ICollection<CartDetailsT> CartDetailsT { get; set; } = new List<CartDetailsT>();

    public virtual DepartmentSub1T Department { get; set; }

    public virtual ICollection<FavouriteServiceT> FavouriteServiceT { get; set; } = new List<FavouriteServiceT>();

    public virtual ICollection<RequestServicesT> RequestServicesT { get; set; } = new List<RequestServicesT>();

    public virtual ICollection<SubscriptionServiceT> SubscriptionServiceT { get; set; } = new List<SubscriptionServiceT>();
}
