using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ClientT
    {
        public ClientT()
        {
            AddressT = new HashSet<AddressT>();
            CleaningSubscribersT = new HashSet<CleaningSubscribersT>();
            ClientPhonesT = new HashSet<ClientPhonesT>();
            ClientPointT = new HashSet<ClientPointT>();
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
        public ICollection<CleaningSubscribersT> CleaningSubscribersT { get; set; }
        public ICollection<ClientPhonesT> ClientPhonesT { get; set; }
        public ICollection<ClientPointT> ClientPointT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<SiteT> SiteT { get; set; }
    }
}
