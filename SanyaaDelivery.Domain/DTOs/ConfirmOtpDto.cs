using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ConfirmOtpDto
    {
        public int AccountId { get; set; }
        public string OTPCode { get; set; }
    }
}
