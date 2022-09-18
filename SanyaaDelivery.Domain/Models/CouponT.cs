using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CouponT
    {
        public CouponT()
        {
            CouponCityT = new HashSet<CouponCityT>();
            CouponDepartmentT = new HashSet<CouponDepartmentT>();
        }

        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public sbyte Type { get; set; }
        public int Value { get; set; }
        public int MinimumCharge { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MaxUseCount { get; set; }
        public int UsedCount { get; set; }
        public bool IsForAllDepartments { get; set; }
        public bool IsForAllCities { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? SystemUserId { get; set; }

        public SystemUserT SystemUser { get; set; }
        public ICollection<CouponCityT> CouponCityT { get; set; }
        public ICollection<CouponDepartmentT> CouponDepartmentT { get; set; }
    }
}
