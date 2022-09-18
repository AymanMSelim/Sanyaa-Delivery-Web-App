using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UpdatePasswordDto
    {
        [Required]
        public int AccountId { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        public string Signature { get; set; }
    }
}
