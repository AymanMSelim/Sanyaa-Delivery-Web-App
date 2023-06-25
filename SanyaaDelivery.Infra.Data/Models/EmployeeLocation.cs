using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class EmployeeLocation
{
    public string EmployeeId { get; set; }

    public string Location { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
