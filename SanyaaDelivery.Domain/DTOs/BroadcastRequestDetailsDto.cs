using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class BroadcastRequestDetailsDto
    {
        public int RequestId { get; set; }
        public string RequestCaption { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Status { get; set; }
        public string StatusTranslated { get; set; }
        public List<InvoiceDetailsDto> InvoiceDetails { get; set; }
        public List<RequestServiceDto> RequestServiceList { get; set; }

    }
}
