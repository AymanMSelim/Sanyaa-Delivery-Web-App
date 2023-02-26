using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class AccountRoleT
    {
        public int AccountRoleId { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public bool? IsAcive { get; set; }

        public AccountT Account { get; set; }
        public RoleT Role { get; set; }
    }
}
