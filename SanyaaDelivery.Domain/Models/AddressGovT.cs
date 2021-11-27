using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class AddressGovT
    {
        public AddressGovT()
        {
            AddressCityT = new HashSet<AddressCityT>();
        }

        public int GovId { get; set; }
        public string GovName { get; set; }

        public ICollection<AddressCityT> AddressCityT { get; set; }
    }
}
