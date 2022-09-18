using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CartDetailsT
    {
        public int CartDetailsId { get; set; }
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int? ServiceQuantity { get; set; }
        public DateTime? CreationTime { get; set; }

        public CartT Cart { get; set; }
        public ServiceT Service { get; set; }
    }
}
