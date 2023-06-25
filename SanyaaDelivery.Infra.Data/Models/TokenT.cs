using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class TokenT
{
    public int TokenId { get; set; }

    public string Token { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? AccountId { get; set; }

    public virtual AccountT Account { get; set; }
}
