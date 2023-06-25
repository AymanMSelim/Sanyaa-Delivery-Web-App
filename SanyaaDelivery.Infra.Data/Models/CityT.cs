using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class CityT
{
    public int CityId { get; set; }

    public int? BranchId { get; set; }

    public string CityName { get; set; }

    public short? DeliveryPrice { get; set; }

    public short? MinimumCharge { get; set; }

    public string LoactionLat { get; set; }

    public string LocationLang { get; set; }

    public string LocationUrl { get; set; }

    public int? GovernorateId { get; set; }

    public virtual ICollection<AddressT> AddressT { get; set; } = new List<AddressT>();

    public virtual BranchT Branch { get; set; }

    public virtual GovernorateT Governorate { get; set; }

    public virtual ICollection<OpeningSoonDepartmentT> OpeningSoonDepartmentT { get; set; } = new List<OpeningSoonDepartmentT>();

    public virtual ICollection<PromocodeCityT> PromocodeCityT { get; set; } = new List<PromocodeCityT>();

    public virtual ICollection<RegionT> RegionT { get; set; } = new List<RegionT>();

    public virtual ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; } = new List<ServiceRatioDetailsT>();
}
