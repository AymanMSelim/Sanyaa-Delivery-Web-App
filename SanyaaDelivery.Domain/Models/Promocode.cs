using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class Promocode
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string DateEx { get; set; }
        public string NumMax { get; set; }
        public string NumNow { get; set; }
        public string MinimumCharge { get; set; }
        public string DisAmount { get; set; }
        public string DisPercent { get; set; }
        public string UserId { get; set; }
        public int PromocodeId { get; set; }
    }
}
