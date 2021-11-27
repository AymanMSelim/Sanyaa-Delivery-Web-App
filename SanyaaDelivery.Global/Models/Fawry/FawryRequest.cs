using System;
using System.Collections.Generic;
using System.Text;
using static App.Global.Eumns;

namespace App.Global.Models.Fawry
{
    public class FawryRequest
    {
        public string MerchantCode { get; set; }
        public int MerchantRefNum { get; set; }
        public int CustomerProfileId { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public float Amount { get; set; }
        public long PaymentExpiry { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string CurrencyCode { get; set; }
        public List<FawtyChargeItem> ChargeItems { get; set; }
        public string Signature { get; set; }
    }

    public class FawtyChargeItem
    {
        public string ItemId { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

}
