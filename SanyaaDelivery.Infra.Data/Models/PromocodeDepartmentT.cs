using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PromocodeDepartmentT
{
    public int PromocodeDepartmentJd { get; set; }

    public int? DepartmentId { get; set; }

    public int? PromocodeId { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual PromocodeT Promocode { get; set; }
}
