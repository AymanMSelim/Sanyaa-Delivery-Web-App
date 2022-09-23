using System;
using System.Collections.Generic;
using System.Text;
using static App.Global.Enums;

namespace App.Global.Models.Fawry
{
    public class FawryRefNumberResponse
    {
        public string Type { get; set; }
        public string ReferenceNumber { get; set; }
        public string MerchantRefNumber { get; set; }
        public float OrderAmount { get; set; }
        public float PaymentAmount { get; set; }
        public float FawryFees { get; set; }
        public FawryPaymentMethod PaymentMethod { get; set; }
        public FawryRequestStatus OrderStatus { get; set; }
        public long PaymentTime { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerProfileId { get; set; }
        public string Signature { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}
