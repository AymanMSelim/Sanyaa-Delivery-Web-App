using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DiscountTypeT
    {
        public DiscountTypeT()
        {
            GeneralDiscountT = new HashSet<GeneralDiscountT>();
        }

        public int DiscountTypeId { get; set; }
        public string DiscountTypeName { get; set; }
        public string DiscountTypeDes { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<GeneralDiscountT> GeneralDiscountT { get; set; }
    }
}
