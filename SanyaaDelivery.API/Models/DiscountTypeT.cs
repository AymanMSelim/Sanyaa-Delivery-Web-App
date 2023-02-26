using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class DiscountTypeT
    {
        public DiscountTypeT()
        {
            RequestDiscountT = new HashSet<RequestDiscountT>();
        }

        public sbyte DiscountTypeId { get; set; }
        public string DiscountTypeName { get; set; }
        public string DiscountTypeDes { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<RequestDiscountT> RequestDiscountT { get; set; }
    }
}
