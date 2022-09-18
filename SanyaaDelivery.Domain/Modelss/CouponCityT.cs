using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CouponCityT
    {
        public int CouponCityId { get; set; }
        public int CouponId { get; set; }
        public int CityId { get; set; }

        public CityT City { get; set; }
        public CouponT Coupon { get; set; }
    }
}
