using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestServicesT
    {
        public int RequestServiceId { get; set; }
        public int RequestId { get; set; }
        public int? ServiceId { get; set; }
        public sbyte RequestServicesQuantity { get; set; }
        public DateTime? AddTimestamp { get; set; }
        public short? RequestServiceCost { get; set; }
        public short? RequestServiceDiscount { get; set; }
        public short? RequestServiceMaterial { get; set; }
        public short? RequestServicePoint { get; set; }

        public RequestT Request { get; set; }
        public ServiceT Service { get; set; }
    }
}
