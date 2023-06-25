using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AddressT
{
    public int AddressId { get; set; }

    public int ClientId { get; set; }

    public int? GovernorateId { get; set; }

    public int? CityId { get; set; }

    public int? RegionId { get; set; }

    public string AddressGov { get; set; }

    public string AddressCity { get; set; }

    public string AddressRegion { get; set; }

    public string AddressStreet { get; set; }

    public short? AddressBlockNum { get; set; }

    public short? AddressFlatNum { get; set; }

    public string AddressDes { get; set; }

    public string Location { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public ulong IsDefault { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual CityT City { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual GovernorateT Governorate { get; set; }

    public virtual RegionT Region { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();
}
