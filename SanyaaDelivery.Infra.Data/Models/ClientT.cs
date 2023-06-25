using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ClientT
{
    public int ClientId { get; set; }

    public string ClientName { get; set; }

    public int? CurrentAddress { get; set; }

    public string CurrentPhone { get; set; }

    public string ClientEmail { get; set; }

    public DateTime? ClientRegDate { get; set; }

    public string ClientNotes { get; set; }

    public string ClientKnowUs { get; set; }

    public int? BranchId { get; set; }

    public int? SystemUserId { get; set; }

    public int ClientPoints { get; set; }

    public ulong IsGuest { get; set; }

    public virtual ICollection<AddressT> AddressT { get; set; } = new List<AddressT>();

    public virtual BranchT Branch { get; set; }

    public virtual ICollection<CartT> CartT { get; set; } = new List<CartT>();

    public virtual ICollection<Cleaningsubscribers> Cleaningsubscribers { get; set; } = new List<Cleaningsubscribers>();

    public virtual ICollection<ClientPhonesT> ClientPhonesT { get; set; } = new List<ClientPhonesT>();

    public virtual ICollection<ClientPointT> ClientPointT { get; set; } = new List<ClientPointT>();

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual ICollection<EmployeeReviewT> EmployeeReviewT { get; set; } = new List<EmployeeReviewT>();

    public virtual ICollection<FavouriteEmployeeT> FavouriteEmployeeT { get; set; } = new List<FavouriteEmployeeT>();

    public virtual ICollection<FavouriteServiceT> FavouriteServiceT { get; set; } = new List<FavouriteServiceT>();

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SiteT> SiteT { get; set; } = new List<SiteT>();

    public virtual SystemUserT SystemUser { get; set; }
}
