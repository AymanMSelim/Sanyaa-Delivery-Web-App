using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public DateTime? OrderTime { get; set; }

        public float? OrderCost { get; set; }

        public int OrderStatus { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientPhone { get; set; }

        public string BranchName { get; set; }

        public bool IsCanceled { get; set; }
    }
}
