using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ClientPhonesT
    {
        public int ClientId { get; set; }
        public string ClientPhone { get; set; }
        public string PwdUsr { get; set; }
        public string Code { get; set; }
        public sbyte? Active { get; set; }
        public bool? IsDefault { get; set; }
        public int ClientPhoneId { get; set; }

        public ClientT Client { get; set; }
    }
}
