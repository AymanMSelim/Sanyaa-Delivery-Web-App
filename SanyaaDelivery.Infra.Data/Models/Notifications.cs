using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Notifications
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }
}
