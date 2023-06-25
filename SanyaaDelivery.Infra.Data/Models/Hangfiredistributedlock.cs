using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfiredistributedlock
{
    public string Resource { get; set; }

    public DateTime CreatedAt { get; set; }
}
