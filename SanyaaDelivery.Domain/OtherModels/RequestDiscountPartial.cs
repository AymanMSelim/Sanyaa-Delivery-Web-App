using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestDiscountT
    {
        public static RequestDiscountT ReturnPromocodeDiscount(decimal discountValue, decimal companyPercentage)
        {
            return new RequestDiscountT
            {
                CompanyPercentage = companyPercentage,
                CreationTime = DateTime.Now,
                Description = "خصم بروموكود",
                DiscountTypeId = 1,
                DiscountValue = discountValue,
            };
        }

        public static RequestDiscountT ReturnPointDiscount(decimal discountValue, decimal companyPercentage)
        {
            return new RequestDiscountT
            {
                CompanyPercentage = companyPercentage,
                CreationTime = DateTime.Now,
                Description = "خصم نقاط",
                DiscountTypeId = 2,
                DiscountValue = discountValue,
            };
        }

        public static RequestDiscountT ReturnServiceDiscount(decimal discountValue, decimal companyPercentage)
        {
            return new RequestDiscountT
            {
                CompanyPercentage = companyPercentage,
                CreationTime = DateTime.Now,
                Description = "خصم خدمات",
                DiscountTypeId = 3,
                DiscountValue = discountValue,
            };
        }

        public static RequestDiscountT ReturnOtherDiscount(decimal discountValue, decimal companyPercentage)
        {
            return new RequestDiscountT
            {
                CompanyPercentage = companyPercentage,
                CreationTime = DateTime.Now,
                Description = "خصم أخر",
                DiscountTypeId = 100,
                DiscountValue = discountValue,
            };
        }
    }
}
