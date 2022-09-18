using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class TokenT
    {
        public int TokenId { get; set; }
        public string Token { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? AccountId { get; set; }

        public AccountT Account { get; set; }
    }
}
