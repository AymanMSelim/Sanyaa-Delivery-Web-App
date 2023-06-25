using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AccountTypeT
{
    public int AccountTypeId { get; set; }

    public string AccountTypeName { get; set; }

    public string AccountTypeDes { get; set; }

    public ulong? IsActive { get; set; }

    public virtual ICollection<AccountT> AccountT { get; set; } = new List<AccountT>();
}
