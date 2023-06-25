using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfirejobstate
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }

    public string Reason { get; set; }

    public string Data { get; set; }

    public virtual Hangfirejob Job { get; set; }
}
