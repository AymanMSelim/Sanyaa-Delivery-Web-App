using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AccountRoleT
{
    public int AccountRoleId { get; set; }

    public int RoleId { get; set; }

    public int AccountId { get; set; }

    public ulong? IsAcive { get; set; }

    public virtual AccountT Account { get; set; }

    public virtual RoleT Role { get; set; }
}
