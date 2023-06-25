using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ClientPhonesT
{
    public int ClientPhoneId { get; set; }

    public int? ClientId { get; set; }

    public string ClientPhone { get; set; }

    public string PwdUsr { get; set; }

    public string Code { get; set; }

    public sbyte? Active { get; set; }

    public ulong IsDefault { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();
}
