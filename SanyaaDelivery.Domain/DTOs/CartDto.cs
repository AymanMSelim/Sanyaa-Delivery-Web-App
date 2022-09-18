using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int ClientId { get; set; }
        public string Note { get; set; }
        public bool IsPromoCodeApplied { get; set; }
        public string PromoCode { get; set; }
        public decimal PromoCodeDiscount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ServiceDiscount { get; set; }
        public decimal DepartmentDiscount { get; set; }
        public int SubscriptionId { get; set; }
        public decimal SubscriptionDiscountPercentage { get; set; }
        public decimal SubscriptionDiscount { get; set; }
        public decimal MaxDiscountPercentage { get; set; }
        public string MaxDiscountPercentageDescription { get; set; }
        public decimal ApplliedDiscount { get; set; }
        public int TotalPoints { get; set; }
        public int PointsPerEGP { get; set; }
        public bool UsePoints { get; set; }
        public decimal UsedPointsAmountInEGP { get; set; }
        public int UsedPoints { get; set; }
        public string PointDescription { get; set; }
        public string GainPointsDescription { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal MinimumCharge { get; set; }
        public string MinimumChargeDescription { get; set; }
        public int GainPoints { get; set; }
        public decimal ServiceRatio { get; set; }
        public int CityId { get; set; }
        public int DepartmentId { get; set; }
        public decimal PromocodeCompanyDiscountPercentage { get; set; }
        public decimal CompanyDiscount { get; set; }
        public decimal EmployeeDiscount { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal OtherDiscount { get; set; }
        public int AddressId { get; set; }
        public string EmployeeId { get; set; }
        public int PhoneId { get; set; }
        public decimal PointsCompanyDiscountPercentage { get; set; }
        public List<CartServiceDetail> CartServiceDetails { get; set; }
        public List<RequestDiscountT> RequestDiscounts { get; set; }
        public List<AttachmentT> AttachmentList { get; set; }

        public Dictionary<string, decimal> DiscountDetails { get; set; }

        public CartDto()
        {
            RequestDiscounts = new List<RequestDiscountT>();
        }
        public void SetServiceDetails(List<CartDetailsT> cartDetailList)
        {
            CartServiceDetails = new List<CartServiceDetail>();
            foreach (var item in cartDetailList)
            {
                var cartService = new CartServiceDetail
                {
                    CartId = item.CartId,
                    CartDetailsId = item.CartDetailsId,
                    CreationTime = item.CreationTime,
                    ServiceId = item.ServiceId,
                    Service = item.Service,
                    ServiceQuantity = item.ServiceQuantity,
                };
                CartServiceDetails.Add(cartService);
            }
            CalculateServiceDiscount();
        }

        public void CalculateServiceDiscount()
        {
            decimal companyDiscount = 0;
            decimal companyDiscountPercentage = 0;
            if (CartServiceDetails != null && CartServiceDetails.Count > 0)
            {
                foreach (var item in CartServiceDetails)
                {
                    item.ServiceName = item.Service.ServiceName;
                    item.OriginalPrice = item.Service.ServiceCost;
                    item.Price = (decimal)item.Service.ServiceCost * ServiceRatio;
                    if(item.Service.NoDiscount.HasValue && item.Service.NoDiscount.Value == false && item.Service.ServiceDiscount.HasValue && item.ServiceQuantity >= item.Service.DiscountServiceCount)
                    {
                        item.Discount = (decimal)item.Service.ServiceDiscount.Value / 100 * item.Price;
                        companyDiscount += item.Discount * (decimal)item.Service.CompanyDiscountPercentage / 100;
                    }
                    item.Points = item.Service.ServicePoints.Value * item.ServiceQuantity.Value;
                    item.NetPrice = item.Price - item.Discount;
                    item.TotalDiscount = item.Discount * (decimal)item.ServiceQuantity;
                    item.TotalPrice = item.Price * (decimal)item.ServiceQuantity;
                    item.TotalNetPrice = item.TotalPrice - item.TotalDiscount;
                    MaterialCost += (decimal)item.Service.MaterialCost * (decimal)item.ServiceQuantity;
                    item.Service = null;
                }
                TotalPrice = CartServiceDetails.Sum(d => d.Price * d.ServiceQuantity.Value);
                ServiceDiscount = CartServiceDetails.Sum(d => d.TotalDiscount);
                GainPoints = CartServiceDetails.Sum(d => d.Points);
                NetPrice = TotalPrice - ServiceDiscount;
                CompanyDiscount += companyDiscount;
                EmployeeDiscount += ServiceDiscount - companyDiscount;
                if(ServiceDiscount > 0)
                {
                    companyDiscountPercentage = (companyDiscount / ServiceDiscount) * 100;
                    RequestDiscounts.Add(RequestDiscountT.ReturnServiceDiscount(ServiceDiscount, companyDiscountPercentage));
                }
            }
        }
        
        public void SetDeliveryPrice()
        {
            NetPrice += DeliveryPrice;
        }
        public void SetTax(int taxPercentage)
        {
            Tax = (taxPercentage / 100) * NetPrice;
            NetPrice += Tax;
        }

        public void CalculateDepartmentDiscount(int departmentPercentage)
        {
            DepartmentDiscount = (departmentPercentage / 100) * NetPrice;
            NetPrice -= DepartmentDiscount;
        }  
        
        public void CalculateSubscriptionDiscount(int subscriptionDiscountPercentage)
        {
            SubscriptionDiscount = (subscriptionDiscountPercentage / 100) * NetPrice;
            NetPrice -= SubscriptionDiscount;
        }

        public void CalculatePromocodeDiscount(PromocodeT promocode)
        {
            if(promocode == null)
            {
                IsPromoCodeApplied = false;
                PromoCode = null;
                PromoCodeDiscount = 0;
                return;
            }
            if(promocode.MinimumCharge > TotalPrice)
            {
                return;
            }
            IsPromoCodeApplied = true;
            PromoCode = promocode.Promocode;
            if(promocode.Type == 1) //value
            {
                PromoCodeDiscount = promocode.Value;
            }
            else
            {
                PromoCodeDiscount = (promocode.Value / 100) * NetPrice;
            }
            CompanyDiscount += promocode.CompanyDiscountPercentage / 100 * PromoCodeDiscount;
            EmployeeDiscount += (100 - promocode.CompanyDiscountPercentage) / 100 * PromoCodeDiscount;
            NetPrice -= PromoCodeDiscount;
            if (PromoCodeDiscount > 0)
            {
                RequestDiscounts.Add(RequestDiscountT.ReturnPromocodeDiscount(PromoCodeDiscount, promocode.CompanyDiscountPercentage));
            }
        }

        public void CalculateAppliedDiscount()
        {
            if(TotalPrice == 0 || NetPrice == 0)
            {
                return;
            }
            decimal currentDiscountPercentage = ((TotalPrice - NetPrice) / TotalPrice * 100);
            decimal maxDiscount = TotalPrice * MaxDiscountPercentage / 100;
            var currentDiscount = TotalPrice * currentDiscountPercentage / 100;
            if (currentDiscountPercentage > MaxDiscountPercentage)
            {
                ApplliedDiscount = maxDiscount;
            }
            else
            {
                ApplliedDiscount = currentDiscount; ;
            }
            NetPrice = TotalPrice - ApplliedDiscount;
        }

        public void CalculateUsedPoints()
        {
            var maxDiscount = TotalPrice * MaxDiscountPercentage / 100;
            if (ApplliedDiscount < maxDiscount)
            {
                decimal pointsDiscount = 0;
                if (UsePoints)
                {
                    var avaAdditionalDiscount = maxDiscount - ApplliedDiscount;
                    var pointsEGPAmount = TotalPoints / PointsPerEGP;
                    if (pointsEGPAmount > avaAdditionalDiscount)
                    {
                        UsedPoints = (int)(avaAdditionalDiscount * PointsPerEGP);
                        pointsDiscount = avaAdditionalDiscount;
                    }
                    else
                    {
                        UsedPoints = TotalPoints;
                        pointsDiscount = pointsEGPAmount;
                    }
                }
                ApplliedDiscount += pointsDiscount;
                NetPrice = TotalPrice - ApplliedDiscount;
                UsedPointsAmountInEGP = pointsDiscount;
                CompanyDiscount += pointsDiscount * PointsCompanyDiscountPercentage / 100;
                EmployeeDiscount += pointsDiscount * (100 - PointsCompanyDiscountPercentage) / 100;
                RequestDiscounts.Add(RequestDiscountT.ReturnPointDiscount(pointsDiscount, PointsCompanyDiscountPercentage));
            }
        }

        public void CheckMinimumCharge()
        {
            if(NetPrice < MinimumCharge)
            {
                NetPrice = MinimumCharge;
            }
        }
        public void CalculateCompanyEmployeeDiscount()
        {
            var totalDiscount = EmployeeDiscount + CompanyDiscount;
            if(ApplliedDiscount != totalDiscount)
            {
                 var percentage = ApplliedDiscount / totalDiscount;
                EmployeeDiscount = percentage * EmployeeDiscount;
                CompanyDiscount = percentage * CompanyDiscount;
            }
        }

        public void RoundDecimal()
        {
            NetPrice = Math.Round(NetPrice, 2, MidpointRounding.AwayFromZero);
            TotalPrice = Math.Round(TotalPrice, 2, MidpointRounding.AwayFromZero);
            ApplliedDiscount = Math.Round(ApplliedDiscount, 2, MidpointRounding.AwayFromZero);
            CompanyDiscount = Math.Round(CompanyDiscount, 2, MidpointRounding.AwayFromZero);
            EmployeeDiscount = Math.Round(EmployeeDiscount, 2, MidpointRounding.AwayFromZero);
            UsedPointsAmountInEGP = Math.Round(UsedPointsAmountInEGP, 2, MidpointRounding.AwayFromZero);
        }

        public void SetDescription()
        {
            PointDescription = $"رصيد نقاطك : {TotalPoints} نقطة = {TotalPoints / PointsPerEGP}ج";
            GainPointsDescription = $"ستحصل على {GainPoints} نقطه بعد استكمال الطلب";
            MinimumChargeDescription = $"الحد الأدنى للطلب {MinimumCharge}ج";
            MaxDiscountPercentageDescription = $"الحد الأقصى للخصم المطبق {MaxDiscountPercentage}% من قيمة الطلب";
        }

        public void CalcualteDiscountDetails()
        {
            DiscountDetails = new Dictionary<string, decimal>();
            foreach (var item in RequestDiscounts)
            {
                DiscountDetails.Add(item.Description, Math.Round(item.DiscountValue, 2));
            }
            DiscountDetails.Add("إجمالى الخصم", Math.Round(RequestDiscounts.Sum(d => d.DiscountValue), 2));
            DiscountDetails.Add($"أقصى نسبة خصم ({MaxDiscountPercentage}%)", Math.Round(TotalPrice * MaxDiscountPercentage /100, 2));
            DiscountDetails.Add("الخصم المطبق", ApplliedDiscount);
        }
    }
}
