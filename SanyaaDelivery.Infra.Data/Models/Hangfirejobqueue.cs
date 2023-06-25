using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfirejobqueue
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public DateTime? FetchedAt { get; set; }

    public string Queue { get; set; }

    public string FetchToken { get; set; }
}
