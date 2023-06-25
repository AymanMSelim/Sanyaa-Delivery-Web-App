using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfirejob
{
    public int Id { get; set; }

    public int? StateId { get; set; }

    public string StateName { get; set; }

    public string InvocationData { get; set; }

    public string Arguments { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpireAt { get; set; }

    public virtual ICollection<Hangfirejobparameter> Hangfirejobparameter { get; set; } = new List<Hangfirejobparameter>();

    public virtual ICollection<Hangfirejobstate> Hangfirejobstate { get; set; } = new List<Hangfirejobstate>();

    public virtual ICollection<Hangfirestate> Hangfirestate { get; set; } = new List<Hangfirestate>();
}
