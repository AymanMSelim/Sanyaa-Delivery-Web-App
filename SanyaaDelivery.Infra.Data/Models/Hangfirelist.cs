using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfirelist
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public DateTime? ExpireAt { get; set; }
}
