using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ResetPasswordDto
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string SecurityCode { get; set; }
    }
}
