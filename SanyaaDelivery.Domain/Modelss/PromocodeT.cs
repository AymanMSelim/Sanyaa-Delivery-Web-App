﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class PromocodeT
    {
        public PromocodeT()
        {
            PromocodeCityT = new HashSet<PromocodeCityT>();
            PromocodeDepartmentT = new HashSet<PromocodeDepartmentT>();
        }

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
        public int SystemUserId { get; set; }

        public SystemUserT SystemUser { get; set; }
        public ICollection<PromocodeCityT> PromocodeCityT { get; set; }
        public ICollection<PromocodeDepartmentT> PromocodeDepartmentT { get; set; }
    }
}