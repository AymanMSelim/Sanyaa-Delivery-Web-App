using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfireserver
{
    public string Id { get; set; }

    public string Data { get; set; }

    public DateTime? LastHeartbeat { get; set; }
}
