using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ClientPhonesT
    {
        public ClientPhonesT()
        {
            RequestT = new HashSet<RequestT>();
        }

        public int ClientPhoneId { get; set; }
        public int? ClientId { get; set; }
        public string ClientPhone { get; set; }
        public string PwdUsr { get; set; }
        public string Code { get; set; }
        public sbyte? Active { get; set; }

        public ClientT Client { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
    }
}
