using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UpdateRequestQuantityDto
    {
        public int RequestServiceId { get; set; }
        public int ServiceQuantity { get; set; }
    }
}
