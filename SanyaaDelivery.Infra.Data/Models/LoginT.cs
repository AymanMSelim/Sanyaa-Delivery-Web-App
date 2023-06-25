using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class LoginT
{
    public string EmployeeId { get; set; }

    public DateTime? LastActiveTimestamp { get; set; }

    public string LoginPassword { get; set; }

    public ulong LoginAccountState { get; set; }

    public string LoginAvailability { get; set; }

    public string LoginMessage { get; set; }

    public string LoginAccountDeactiveMessage { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
