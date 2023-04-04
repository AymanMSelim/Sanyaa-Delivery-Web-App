using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientPointDto
    {
        public int? Points { get; set; }
        public sbyte? PointType { get; set; }
        public string PointTypeDescription { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
    }
}
