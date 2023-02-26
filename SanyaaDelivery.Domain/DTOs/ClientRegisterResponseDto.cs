using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientRegisterResponseDto
    {
        public ClientT Client { get; set; }
        public string OtpCode { get; set; }
        public string SecurityCode { get; set; }
        public DateTime OTPExpireTime { get; set; }
    }

    public class GuestClientRegisterResponseDto
    {
        public ClientT Client { get; set; }
       public AccountT Account { get; set; }
    }
}
