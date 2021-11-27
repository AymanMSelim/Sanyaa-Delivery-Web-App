using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string RefernceId { get; set; }
        public string AddressGov { get; set; }
        public string AddressCity { get; set; }
        public string AddressRegion { get; set; }
        public string AddressStreet { get; set; }
        public short? AddressBlockNum { get; set; }
        public short? AddressFlatNum { get; set; }
        public string AddressDescreption { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationText { get; set; }
    }
}
