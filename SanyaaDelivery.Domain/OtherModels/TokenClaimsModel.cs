using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.OtherModels
{
    public class TokenClaimsModel
    {
        public int? AccountId { get; set; }
        public int SystemUserId { get; set; }
        public int? AccountTypeId { get; set; }
        public string ReferenceId { get; set; }
    }
}
