using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeAppPaymentIndexDto
    {
        public string EmployeeAmountPercentageCaption { get; set; }
        public string EmployeeAmountPercentage { get; set; }
        public string EmployeeAmountPercentageDescription { get; set; }
        public List<EmployeeAppPaymentRequestItemDto> RequestList { get; set; }
    }

    public class EmployeeAppPaymentRequestItemDto
    {
        public int RequestId { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string RequestCaption { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string EmployeeAmountPercentageDescription { get; set; }
        public string CompanyAmountPercentageDescription { get; set; }
        public string DuoDateDecription { get; set; }
    }
}
