using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class ClientT
    {
        public ClientT()
        {
            AddressT = new HashSet<AddressT>();
            CartT = new HashSet<CartT>();
            Cleaningsubscribers = new HashSet<Cleaningsubscribers>();
            ClientPhonesT = new HashSet<ClientPhonesT>();
            ClientPointT = new HashSet<ClientPointT>();
            ClientSubscriptionT = new HashSet<ClientSubscriptionT>();
            EmployeeReviewT = new HashSet<EmployeeReviewT>();
            FavouriteEmployeeT = new HashSet<FavouriteEmployeeT>();
            FavouriteServiceT = new HashSet<FavouriteServiceT>();
            RequestT = new HashSet<RequestT>();
            SiteT = new HashSet<SiteT>();
        }

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
        public int? ClientPoints { get; set; }

        public BranchT Branch { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<AddressT> AddressT { get; set; }
        public ICollection<CartT> CartT { get; set; }
        public ICollection<Cleaningsubscribers> Cleaningsubscribers { get; set; }
        public ICollection<ClientPhonesT> ClientPhonesT { get; set; }
        public ICollection<ClientPointT> ClientPointT { get; set; }
        public ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public ICollection<EmployeeReviewT> EmployeeReviewT { get; set; }
        public ICollection<FavouriteEmployeeT> FavouriteEmployeeT { get; set; }
        public ICollection<FavouriteServiceT> FavouriteServiceT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SiteT> SiteT { get; set; }
    }
}
