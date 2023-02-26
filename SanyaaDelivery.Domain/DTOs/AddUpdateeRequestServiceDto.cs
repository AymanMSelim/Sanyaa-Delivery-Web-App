using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AddUpdateeRequestServiceDto
    {
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceQuantity { get; set; }
    }
}
