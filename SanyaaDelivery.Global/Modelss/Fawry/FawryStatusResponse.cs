using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.Models.Fawry
{
    public class FawryStatusResponse
    {
        public string Type { get; set; }
        public string ReferenceNumber { get; set; }
        public string MerchantRefNumber { get; set; }
        public decimal PaymentAmount { get; set; }
        public long ExpirationTime { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string Signature { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public bool BasketPayment { get; set; }

    }
}
