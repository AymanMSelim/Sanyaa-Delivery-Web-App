using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class OTPCodeDto
    {
        public string OTPCode { get; set; }
        public DateTime OTPExpireTime { get; set; }
    }
}
