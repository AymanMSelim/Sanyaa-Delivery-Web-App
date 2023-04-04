using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.Models.Fawry
{
    public class FawryNotificationCallback
    {
        public string RequestId { get; set; }
        public string FawryRefNumber { get; set; }
        public string MerchantRefNumber { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerMail { get; set; }
        public double PaymentAmount { get; set; }
        public double OrderAmount { get; set; }
        public double FawryFees { get; set; }
        public object ShippingFees { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string MessageSignature { get; set; }
        public long OrderExpiryDate { get; set; }

    }
}
