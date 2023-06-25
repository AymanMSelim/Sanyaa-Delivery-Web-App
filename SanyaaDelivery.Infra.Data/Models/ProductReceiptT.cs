using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ProductReceiptT
{
    public int ReceiptId { get; set; }

    public DateTime? ReceiptTimestamp { get; set; }

    public string ReceiptEmployeeBuyer { get; set; }

    public float? ProductReceiptPaid { get; set; }

    public string SystemUsername { get; set; }

    public int? BranchId { get; set; }

    public virtual ICollection<ProductSoldT> ProductSoldT { get; set; } = new List<ProductSoldT>();
}
