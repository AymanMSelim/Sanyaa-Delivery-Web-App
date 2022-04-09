using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ProductT
    {
        public ProductT()
        {
            ProductSoldT = new HashSet<ProductSoldT>();
            QuantityHistoryT = new HashSet<QuantityHistoryT>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPriceBuy { get; set; }
        public float ProductPriceSell { get; set; }
        public float ProductCustomerPrice { get; set; }
        public short ProductQuantity { get; set; }
        public string ProductDepartment { get; set; }
        public string ProductDes { get; set; }
        public int BranchId { get; set; }

        public BranchT Branch { get; set; }
        public ICollection<ProductSoldT> ProductSoldT { get; set; }
        public ICollection<QuantityHistoryT> QuantityHistoryT { get; set; }
    }
}
