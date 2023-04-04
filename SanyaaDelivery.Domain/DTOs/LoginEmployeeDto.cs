using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class LoginEmployeeDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FCMToken { get; set; }

    }
}
