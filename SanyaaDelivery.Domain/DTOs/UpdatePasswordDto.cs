using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UpdatePasswordDto
    {
        public int AccountId { get; set; }

        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
        public string Signature { get; set; }
    }
}
