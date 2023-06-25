using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RoleT
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public string RoleDes { get; set; }

    public ulong? IsActive { get; set; }

    public virtual ICollection<AccountRoleT> AccountRoleT { get; set; } = new List<AccountRoleT>();
}
