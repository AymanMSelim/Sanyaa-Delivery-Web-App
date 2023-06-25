using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DepartmentSub0T
{
    public string DepartmentName { get; set; }

    public string DepartmentSub0 { get; set; }

    public int DepartmentSub0Id { get; set; }

    public int? DepartmentId { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual ICollection<DepartmentSub1T> DepartmentSub1T { get; set; } = new List<DepartmentSub1T>();

    public virtual ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; } = new List<LandingScreenItemDetailsT>();
}
