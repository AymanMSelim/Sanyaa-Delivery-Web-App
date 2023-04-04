using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientSubscriptionItemDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public int AddressCount { get; set; }
        public int PhoneCount { get; set; }
        public int ActiveSunscriptionCount { get; set; }
    }
}
