using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class PartinerCartT
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string SystemUsername { get; set; }
        public int ServiceCount { get; set; }
    }
}
