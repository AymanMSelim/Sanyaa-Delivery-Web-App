using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class RegestrationT
    {
        public string RegestrationName { get; set; }
        public string RegestrationDepartment { get; set; }
        public string RegestrationGov { get; set; }
        public string RegestrationCity { get; set; }
        public string RegestrationPhone { get; set; }
        public sbyte RegestrationAge { get; set; }
        public sbyte RegestrationExperience { get; set; }
        public string RegestrationPassword { get; set; }
        public string RegestrationTransport { get; set; }
        public DateTime RegestrationTimestamep { get; set; }
        public string RegestrationView { get; set; }
    }
}
