using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UserLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int? AccountType { get; set; }
    }

    public class ClientLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class ClientLoginWithOtpDto
    {
        public string Phone { get; set; }

        public string OtpCode { get; set; }
    }
}
