using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int UserId { get; set; }
        public int Qte { get; set; }
        public string Note { get; set; }
    }
}
