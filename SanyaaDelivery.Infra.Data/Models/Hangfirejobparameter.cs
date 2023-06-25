using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Hangfirejobparameter
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public virtual Hangfirejob Job { get; set; }
}
