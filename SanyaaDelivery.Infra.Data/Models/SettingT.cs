using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SettingT
{
    public int Id { get; set; }

    public DateTime? FawryPaySendDate { get; set; }

    public ulong? FawryAutopayFlag { get; set; }

    public ulong? FawryAutoUpdateStausFlag { get; set; }
}
