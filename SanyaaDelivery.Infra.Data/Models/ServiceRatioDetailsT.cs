using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ServiceRatioDetailsT
{
    public int ServiceRatioDetailsId { get; set; }

    public int? ServiceRatioId { get; set; }

    public int? CityId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual CityT City { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual ServiceRatioT ServiceRatio { get; set; }
}
