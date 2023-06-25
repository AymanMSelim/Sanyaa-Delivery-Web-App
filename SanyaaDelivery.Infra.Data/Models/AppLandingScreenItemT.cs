using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AppLandingScreenItemT
{
    public int ItemId { get; set; }

    public int? ItemType { get; set; }

    public string ImagePath { get; set; }

    public string Caption { get; set; }

    public int? DepartmentId { get; set; }

    public ulong IsActive { get; set; }

    public ulong HavePackage { get; set; }

    public string ActionLink { get; set; }

    public virtual ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; } = new List<LandingScreenItemDetailsT>();
}
