using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class RenewTokenDto
    {
        public int AccountId { get; set; }
        public string Signature { get; set; }
    }
}
