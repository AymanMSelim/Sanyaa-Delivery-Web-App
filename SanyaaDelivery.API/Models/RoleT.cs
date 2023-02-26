using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class RoleT
    {
        public RoleT()
        {
            AccountRoleT = new HashSet<AccountRoleT>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDes { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<AccountRoleT> AccountRoleT { get; set; }
    }
}
