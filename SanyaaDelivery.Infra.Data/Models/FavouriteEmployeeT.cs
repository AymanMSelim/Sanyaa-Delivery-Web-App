using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FavouriteEmployeeT
{
    public int FavouriteEmployeeId { get; set; }

    public string EmployeeId { get; set; }

    public int ClientId { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
