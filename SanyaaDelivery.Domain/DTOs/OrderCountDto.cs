using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class OrderCountDto
    {
        public DateTime Day { get; set; }

        public string EmployeeId { get; set; }

        public string OrderStatus { get; set; }

        public int Count { get; set; }

    }
}
