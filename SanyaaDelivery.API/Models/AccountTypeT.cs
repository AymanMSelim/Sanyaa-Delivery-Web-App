using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class AccountTypeT
    {
        public AccountTypeT()
        {
            AccountT = new HashSet<AccountT>();
        }

        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public string AccountTypeDes { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<AccountT> AccountT { get; set; }
    }
}
