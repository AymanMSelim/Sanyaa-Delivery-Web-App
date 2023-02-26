using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class ProductSoldT
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public short? ProductSoldQuantity { get; set; }
        public float? ProductSoldPrice { get; set; }
        public string ProductSoldNote { get; set; }

        public ProductT Product { get; set; }
        public ProductReceiptT Receipt { get; set; }
    }
}
