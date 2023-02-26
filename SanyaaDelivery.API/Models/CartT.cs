﻿using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
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
        public bool IsViaApp { get; set; }
        public string Note { get; set; }
        public int? PromocodeId { get; set; }
        public bool UsePoint { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }

        public ClientT Client { get; set; }
        public DepartmentT Department { get; set; }
        public PromocodeT Promocode { get; set; }
        public ICollection<CartDetailsT> CartDetailsT { get; set; }
    }
}
