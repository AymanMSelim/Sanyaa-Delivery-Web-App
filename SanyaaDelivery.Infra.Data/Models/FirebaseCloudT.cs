using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FirebaseCloudT
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string Token { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual AccountT Account { get; set; }
}
