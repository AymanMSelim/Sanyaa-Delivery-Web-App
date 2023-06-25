using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class LandingScreenItemDetailsT
{
    public int Id { get; set; }

    public int? ItemId { get; set; }

    public string DepartmentName { get; set; }

    public int DepartmentId { get; set; }

    public int? DapartmentSub0Id { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public ulong IsActive { get; set; }

    public virtual DepartmentSub0T DapartmentSub0 { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual AppLandingScreenItemT Item { get; set; }
}
