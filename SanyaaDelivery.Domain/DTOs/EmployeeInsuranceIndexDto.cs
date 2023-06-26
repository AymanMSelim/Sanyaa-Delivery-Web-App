using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeInsuranceIndexDto
    {
        public string SubscriptionCaptionTop { get; set; }
        public string SubscriptionCaption { get; set; }
        public string SubscriptionCaptionDown { get; set; }
        public string SubscriptionPaidAmountCaption { get; set; }
        public List<InsurancePaymentDto> PaymentList { get; set; }
    }

    public class InsurancePaymentDto 
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
