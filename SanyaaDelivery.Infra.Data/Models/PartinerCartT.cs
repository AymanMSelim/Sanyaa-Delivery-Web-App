using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PartinerCartT
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public string SystemUsername { get; set; }

    public int ServiceCount { get; set; }
}
