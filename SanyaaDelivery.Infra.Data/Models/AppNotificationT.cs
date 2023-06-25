using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class AppNotificationT
{
    public int NotificationId { get; set; }

    public int AccountId { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public string Link { get; set; }

    public string Image { get; set; }

    public DateTime CreationTime { get; set; }
}
