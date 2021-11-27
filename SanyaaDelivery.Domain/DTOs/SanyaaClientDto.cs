using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SanyaaClientDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public string ClientNotes { get; set; }
        public string ClientKnowUs { get; set; }
        public int? BranchId { get; set; }
        public int SystemUserId { get; set; }
        public List<AddressDto> ClientAddresses { get; set; }
        public List<PhoneDto> ClientPhones { get; set; }
    }
}
