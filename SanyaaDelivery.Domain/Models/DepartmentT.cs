using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentT
    {
        public DepartmentT()
        {
            CartT = new HashSet<CartT>();
            CouponDepartmentT = new HashSet<CouponDepartmentT>();
            DepartmentEmployeeT = new HashSet<DepartmentEmployeeT>();
            DepartmentSub0T = new HashSet<DepartmentSub0T>();
            LandingScreenItemDetailsT = new HashSet<LandingScreenItemDetailsT>();
            OpeningSoonDepartmentT = new HashSet<OpeningSoonDepartmentT>();
            PromocodeDepartmentT = new HashSet<PromocodeDepartmentT>();
            RequestT = new HashSet<RequestT>();
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
            SubscriptionT = new HashSet<SubscriptionT>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public sbyte? DepartmentPercentage { get; set; }
        public string DepartmentDes { get; set; }
        public string DepartmentImage { get; set; }
        public sbyte? MaximumDiscountPercentage { get; set; }

        public ICollection<CartT> CartT { get; set; }
        public ICollection<CouponDepartmentT> CouponDepartmentT { get; set; }
        public ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public ICollection<DepartmentSub0T> DepartmentSub0T { get; set; }
        public ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; }
        public ICollection<OpeningSoonDepartmentT> OpeningSoonDepartmentT { get; set; }
        public ICollection<PromocodeDepartmentT> PromocodeDepartmentT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
        public ICollection<SubscriptionT> SubscriptionT { get; set; }
    }
}
