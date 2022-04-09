using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ProductReceiptT
    {
        public ProductReceiptT()
        {
            ProductSoldT = new HashSet<ProductSoldT>();
        }

        public int ReceiptId { get; set; }
        public DateTime? ReceiptTimestamp { get; set; }
        public string ReceiptEmployeeBuyer { get; set; }
        public float? ProductReceiptPaid { get; set; }
        public string SystemUsername { get; set; }
        public int? BranchId { get; set; }

        public ICollection<ProductSoldT> ProductSoldT { get; set; }
    }
}
