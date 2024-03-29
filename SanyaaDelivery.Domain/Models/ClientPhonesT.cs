﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ClientPhonesT
    {
        public ClientPhonesT()
        {
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            RequestT = new HashSet<RequestT>();
        }

        public int ClientPhoneId { get; set; }
        public int? ClientId { get; set; }
        public string ClientPhone { get; set; }
        public string PwdUsr { get; set; }
        public string Code { get; set; }
        public sbyte? Active { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }

        public ClientT Client { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
    }
}
