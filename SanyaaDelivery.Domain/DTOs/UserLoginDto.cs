using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int? AccountType { get; set; }
    }

    public class ClientLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
