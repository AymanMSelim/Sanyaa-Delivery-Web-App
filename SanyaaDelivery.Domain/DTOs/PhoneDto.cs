using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class PhoneDto
    {
        public int PhoneId { get; set; }
        public string Phone { get; set; }
        public string RefernceId { get; set; }
        public bool IsDefault { get; set; }
    }
}
