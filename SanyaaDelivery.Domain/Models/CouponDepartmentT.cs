using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CouponDepartmentT
    {
        public int CouponDepartmentId { get; set; }
        public int CouponId { get; set; }
        public int DepartmentId { get; set; }

        public CouponT Coupon { get; set; }
        public DepartmentT Department { get; set; }
    }
}
