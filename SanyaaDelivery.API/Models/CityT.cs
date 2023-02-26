using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class CityT
    {
        public CityT()
        {
            AddressT = new HashSet<AddressT>();
            OpeningSoonDepartmentT = new HashSet<OpeningSoonDepartmentT>();
            PromocodeCityT = new HashSet<PromocodeCityT>();
            RegionT = new HashSet<RegionT>();
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
        }

        public int CityId { get; set; }
        public int? BranchId { get; set; }
        public string CityName { get; set; }
        public short? DeliveryPrice { get; set; }
        public short? MinimumCharge { get; set; }
        public string LoactionLat { get; set; }
        public string LocationLang { get; set; }
        public string LocationUrl { get; set; }
        public int? GovernorateId { get; set; }

        public BranchT Branch { get; set; }
        public GovernorateT Governorate { get; set; }
        public ICollection<AddressT> AddressT { get; set; }
        public ICollection<OpeningSoonDepartmentT> OpeningSoonDepartmentT { get; set; }
        public ICollection<PromocodeCityT> PromocodeCityT { get; set; }
        public ICollection<RegionT> RegionT { get; set; }
        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
    }
}
