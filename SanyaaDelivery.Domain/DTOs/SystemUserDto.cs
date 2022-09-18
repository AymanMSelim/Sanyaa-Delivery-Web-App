using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class SystemUserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public object UserData { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public int AccountId { get; set; }
    }
}
