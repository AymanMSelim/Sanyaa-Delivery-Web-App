using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[MaxLength(11)]
        //[DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
