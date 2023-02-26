using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.Domain.DTOs
{
    public class PayRequestDto
    {
        public int RequestId { get; set; }
        public decimal? Amount { get; set; }
    }
}
