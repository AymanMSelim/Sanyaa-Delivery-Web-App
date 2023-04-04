using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeAddressDto
    {
        public string NationalId { get; set; }
        public string Governate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public int BlockNumber { get; set; }
        public int FlatNumber { get; set; }
        public string Lang { get; set; }
        public string Lat { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
