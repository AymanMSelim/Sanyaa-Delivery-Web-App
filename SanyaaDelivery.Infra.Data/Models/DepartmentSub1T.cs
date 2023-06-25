using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DepartmentSub1T
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }

    public string DepartmentSub0 { get; set; }

    public string DepartmentSub1 { get; set; }

    public string DepartmentDes { get; set; }

    public int? DepartmentSub0Id { get; set; }

    public virtual DepartmentSub0T DepartmentSub0Navigation { get; set; }

    public virtual ICollection<ServiceT> ServiceT { get; set; } = new List<ServiceT>();
}
