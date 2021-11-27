using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.Models
{
    public enum PaymentMethod
    {
        PAYATFAWRY
    }
    public class FawryRequest
    {
        public string MarchantCode { get; set; }
        public int MarchantRefNum { get; set; }
        public int CustomerProfileId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public float Amount { get; set; }
        public TimeSpan PaymentExpiry { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<FawtyChargeItem> ChargeItems { get; set; }
        public string Signature { get; set; }
    }


    public class FawtyChargeItem
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
    }
}
