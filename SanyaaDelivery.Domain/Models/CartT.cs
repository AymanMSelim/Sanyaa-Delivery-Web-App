using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class CartT
    {
        public CartT()
        {
            CartDetailsT = new HashSet<CartDetailsT>();
        }

        public int CartId { get; set; }
        public int DepartmentId { get; set; }
        public int ClientId { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }

        public ClientT Client { get; set; }
        public DepartmentT Department { get; set; }
        public ICollection<CartDetailsT> CartDetailsT { get; set; }
    }
}
