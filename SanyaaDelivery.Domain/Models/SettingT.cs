using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class SettingT
    {
        public int Id { get; set; }
        public DateTime? FawryPaySendDate { get; set; }
        public bool? FawryAutopayFlag { get; set; }
        public bool? FawryAutoUpdateStausFlag { get; set; }
    }
}
