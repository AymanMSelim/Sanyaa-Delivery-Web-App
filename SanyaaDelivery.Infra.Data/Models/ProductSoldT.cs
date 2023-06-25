using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ProductSoldT
{
    public int ReceiptId { get; set; }

    public int ProductId { get; set; }

    public short? ProductSoldQuantity { get; set; }

    public float? ProductSoldPrice { get; set; }

    public string ProductSoldNote { get; set; }

    public virtual ProductT Product { get; set; }

    public virtual ProductReceiptT Receipt { get; set; }
}
