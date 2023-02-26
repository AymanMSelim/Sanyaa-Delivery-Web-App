using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class CartServiceDetail : Models.CartDetailsT
    {
        public string ServiceName { get; set; }
        public int Points { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalNetPrice { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal TotalMaterialCost { get; set; }
    }
}
