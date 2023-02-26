using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ServiceCustom : ServiceT
    {
        public bool IsInCart { get; set; }
        public int CartQuantity { get; set; }
        public bool IsFavourite { get; set; }
        public bool HasDiscount { get; set; }
        public decimal NetServiceCost { get; set; }
    }
}
