using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class AppSettingT
    {
        public int SettingId { get; set; }
        public string SettingKey { get; set; }
        public sbyte SettingDatatype { get; set; }
        public string SettingValue { get; set; }
        public bool? IsAppSetting { get; set; }
        public DateTime CreationDate { get; set; }
        public int SystemUserId { get; set; }
    }
}
