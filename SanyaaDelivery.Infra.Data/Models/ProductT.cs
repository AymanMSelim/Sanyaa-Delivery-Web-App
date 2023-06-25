using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ProductT
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public float ProductPriceBuy { get; set; }

    public float ProductPriceSell { get; set; }

    public float ProductCustomerPrice { get; set; }

    public short ProductQuantity { get; set; }

    public string ProductDepartment { get; set; }

    public string ProductDes { get; set; }

    public int BranchId { get; set; }

    public virtual BranchT Branch { get; set; }

    public virtual ICollection<ProductSoldT> ProductSoldT { get; set; } = new List<ProductSoldT>();

    public virtual ICollection<QuantityHistoryT> QuantityHistoryT { get; set; } = new List<QuantityHistoryT>();
}
