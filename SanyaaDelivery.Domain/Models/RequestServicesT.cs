using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestServicesT
    {
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public sbyte RequestServicesQuantity { get; set; }
        public DateTime? AddTimestamp { get; set; }

        public RequestT Request { get; set; }
        public ServiceT Service { get; set; }
    }
}
