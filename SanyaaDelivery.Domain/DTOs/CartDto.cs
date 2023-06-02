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
        public bool IsLessThanMinimumCharge { get; set; }
        public decimal MinimumChargeAdditionAmount { get; private set; }
        public int? PromoCodeId { get; set; }
        public string PromoCode { get; set; }
        public decimal PromoCodeDiscount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ServiceDiscount { get; set; }
        public decimal DepartmentDiscount { get; set; }
        public int ClientSubscriptionId { get; set; }
        public bool IgnoreServiceDiscount { get; set; }
        public decimal SubscriptionDiscountPercentage { get; set; }
        public decimal SubscriptionDiscountCompanyPercentage { get; set; }
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
        public string DepartmentName { get; set; }
        public string DepartmentTerms { get; set; }
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
        public decimal EmployeeDepartmentPercentage { get; set; }
        public decimal EmployeePercentageAmount { get; set; }
        public decimal CompanyePercentageAmount { get; set; }
        public string Address { get; set; }
        public List<CartServiceDetail> CartServiceDetails { get; set; }
        public List<RequestDiscountT> RequestDiscounts { get; set; }
        public List<AttachmentT> AttachmentList { get; set; }

        public Dictionary<string, string> DiscountDetails { get; set; }
        public Dictionary<string, string> InvoiceDetails { get; set; }

        public RequestT Request { get; set; }
        public bool HaveRequest { get; set; }
        public CartDto()
        {
            RequestDiscounts = new List<RequestDiscountT>();
            InvoiceDetails = new Dictionary<string, string>();
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
                    MaterialCost = (decimal)item.Service.MaterialCost,
                    TotalMaterialCost = (decimal)(item.ServiceQuantity * item.Service.ServiceCost)
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
                    if(item.Service.NoDiscount == false 
                        && item.Service.ServiceDiscount.HasValue 
                        && item.ServiceQuantity >= item.Service.DiscountServiceCount
                        && IgnoreServiceDiscount == false)
                    {
                        item.Discount = Math.Round(item.Service.ServiceDiscount.Value / 100 * item.Price, 0);
                        companyDiscount += item.Discount * (decimal)item.Service.CompanyDiscountPercentage / 100;
                    }
                    item.Points = item.Service.ServicePoints.Value * item.ServiceQuantity;
                    item.NetPrice = Math.Round(item.Price - item.Discount, 0);
                    item.TotalDiscount = item.Discount * (decimal)item.ServiceQuantity;
                    item.TotalPrice = item.Price * (decimal)item.ServiceQuantity;
                    item.TotalNetPrice = item.TotalPrice - item.TotalDiscount;
                    MaterialCost += (decimal)item.Service.MaterialCost * (decimal)item.ServiceQuantity;
                    item.Service = null;
                }
                TotalPrice = CartServiceDetails.Sum(d => d.Price * d.ServiceQuantity);
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
            if (NetPrice > 0)
            {
                NetPrice += DeliveryPrice;
            }
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
        
        public void CalculateSubscriptionDiscount()
        {
            SubscriptionDiscount = (SubscriptionDiscountPercentage / 100) * NetPrice;
            NetPrice -= SubscriptionDiscount;
            AddToCompanyDiscount((SubscriptionDiscount * (SubscriptionDiscountCompanyPercentage / 100)));
            AddToEmployeeDiscount(SubscriptionDiscount * (1 - (SubscriptionDiscountCompanyPercentage / 100)));
            if(SubscriptionDiscount > 0)
            {
                RequestDiscounts.Add(RequestDiscountT.ReturnSubscriptionDiscount(SubscriptionDiscount, SubscriptionDiscountCompanyPercentage));
            }
        }

        public void CalculatePromocodeDiscount(PromocodeT promocode)
        {

            if ((promocode.PromocodeDepartmentT.Any(d => d.DepartmentId == DepartmentId) && promocode.PromocodeCityT.Any(d => d.CityId == CityId)) || HaveRequest)
            {
                if (promocode == null)
                {
                    IsPromoCodeApplied = false;
                    PromoCode = null;
                    PromoCodeDiscount = 0;
                    return;
                }
                if (promocode.MinimumCharge > TotalPrice)
                {
                    return;
                }
                IsPromoCodeApplied = true;
                PromoCode = promocode.Promocode;
                PromoCodeId = promocode.PromocodeId;
                if (promocode.Type == (int)Enum.PromocodeType.Value) //value
                {
                    PromoCodeDiscount = promocode.Value;
                }
                else
                {
                    PromoCodeDiscount = ((decimal)promocode.Value / 100) * NetPrice;
                }
                AddToCompanyDiscount(promocode.CompanyDiscountPercentage / 100 * PromoCodeDiscount);
                AddToEmployeeDiscount((100 - promocode.CompanyDiscountPercentage) / 100 * PromoCodeDiscount);
                NetPrice -= PromoCodeDiscount;
                if (PromoCodeDiscount > 0)
                {
                    RequestDiscounts.Add(RequestDiscountT.ReturnPromocodeDiscount(PromoCodeDiscount, promocode.CompanyDiscountPercentage));
                }
            }
        }

        private void AddToCompanyDiscount(decimal value)
        {
            CompanyDiscount += value;
        }

        private void AddToEmployeeDiscount(decimal value)
        {
            EmployeeDiscount += value;
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
            if (HaveRequest)
            {
                TotalPoints += Request.UsedPoints;
            }
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
                IsLessThanMinimumCharge = true;
                MinimumChargeAdditionAmount = NetPrice - MinimumCharge;
                NetPrice = MinimumCharge;
            }
        }
        public void CalculateCompanyEmployeeDiscount()
        {
            var totalDiscount = EmployeeDiscount + CompanyDiscount;
            if(ApplliedDiscount < totalDiscount)
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
            PointDescription = $"رصيد نقاطك : {TotalPoints - UsedPoints} نقطة = {(TotalPoints - UsedPoints) / PointsPerEGP}ج";
            GainPointsDescription = $"ستحصل على {GainPoints} نقطه بعد استكمال الطلب";
            MinimumChargeDescription = $"الحد الأدنى للطلب {MinimumCharge}ج";
            MaxDiscountPercentageDescription = $"الحد الأقصى للخصم المطبق {MaxDiscountPercentage}% من قيمة الطلب";
        }

        public void CalcualteDiscountDetails()
        {
            DiscountDetails = new Dictionary<string, string>();
            DiscountDetails.Add("قيمة الطلب", "+ " + TotalPrice.ToString("0.00"));
            if (NetPrice > 0)
            {
                DiscountDetails.Add("تكلفة الانتقالات", "+ " + DeliveryPrice.ToString("0.00"));
            }
            else
            {
                DiscountDetails.Add("تكلفة الانتقالات", "0.00");
            }
            foreach (var item in RequestDiscounts)
            {
                DiscountDetails.Add(item.Description, Math.Round(item.DiscountValue, 2).ToString("0.00"));
            }
            DiscountDetails.Add("إجمالى الخصم", Math.Round(RequestDiscounts.Sum(d => d.DiscountValue), 2).ToString("0.00"));
            DiscountDetails.Add($"أقصى خصم ({MaxDiscountPercentage}%)", Math.Round(TotalPrice * MaxDiscountPercentage /100, 2).ToString("0.00"));
            DiscountDetails.Add("الخصم المطبق", "- " + ApplliedDiscount.ToString("0.00"));
            DiscountDetails.Add("المبلغ المطلوب", NetPrice.ToString("0.00"));
        }
        public void CalcualteInvoicetDetails()
        {
            InvoiceDetails.Add("الاجمالى", (TotalPrice).ToString("0.00"));
            InvoiceDetails.Add("خصم خدمات", (ServiceDiscount).ToString("0.00"));
            if(NetPrice > 0)
            {
                InvoiceDetails.Add("انتقالات", "+ " + DeliveryPrice.ToString("0.00"));
            }
            else
            {
                InvoiceDetails.Add("انتقالات", "0.00");
            }
            InvoiceDetails.Add("النقاط", UsedPointsAmountInEGP.ToString("0.00"));
            InvoiceDetails.Add("بروموكود", PromoCodeDiscount.ToString("0.00"));
            InvoiceDetails.Add("المبلغ المطلوب", NetPrice.ToString("0.00"));
        }

        public void CalculateAmountPercentage()
        {
            var finalAmount = MinimumChargeAdditionAmount + TotalPrice - MaterialCost;
            //if((NetPrice - DeliveryPrice) > finalAmount)
            //{
            //    finalAmount = NetPrice - DeliveryPrice;
            //}
            EmployeePercentageAmount = finalAmount * (EmployeeDepartmentPercentage / 100) + MaterialCost + DeliveryPrice - EmployeeDiscount;
            CompanyePercentageAmount = finalAmount * (1 - (EmployeeDepartmentPercentage / 100)) - CompanyDiscount;
        }
    }
}
