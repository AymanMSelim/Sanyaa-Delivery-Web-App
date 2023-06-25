using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class OpeningSoonDepartmentT
{
    public int Id { get; set; }

    public int? DepartmentId { get; set; }

    public int? CityId { get; set; }

    public virtual CityT City { get; set; }

    public virtual DepartmentT Department { get; set; }
}
