using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AddressT
    {
        public AddressT()
        {
            RequestT = new HashSet<RequestT>();
        }

        public int AddressId { get; set; }
        public int? ClientId { get; set; }
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

        public ClientT Client { get; set; }
        public RegionT Region { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
    }
}
