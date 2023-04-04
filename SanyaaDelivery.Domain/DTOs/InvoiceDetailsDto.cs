using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class InvoiceDetailsDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool Bold { get; set; }
    }
}
