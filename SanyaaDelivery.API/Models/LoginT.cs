using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class LoginT
    {
        public string EmployeeId { get; set; }
        public DateTime? LastActiveTimestamp { get; set; }
        public string LoginPassword { get; set; }
        public sbyte? LoginAccountState { get; set; }
        public string LoginAvailability { get; set; }
        public string LoginMessage { get; set; }
        public string LoginAccountDeactiveMessage { get; set; }

        public EmployeeT Employee { get; set; }
    }
}
