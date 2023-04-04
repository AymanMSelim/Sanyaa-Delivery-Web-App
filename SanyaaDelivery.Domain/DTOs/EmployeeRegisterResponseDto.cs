using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeRegisterResponseDto
    {
        public EmployeeT Employee { get; set; }
        public string OtpCode { get; set; }
        public string SecurityCode { get; set; }
        public DateTime OTPExpireTime { get; set; }
        public int NextRegisterStep { get; set; }
        public string NextRegisterStepDescription { get; set; }
        public string Token { get; set; }
        public bool IsDataComplete { get; set; }
        public int AccountId { get; set; }
    }

    public class GuestEmployeeRegisterResponseDto
    {
        public EmployeeT Employee { get; set; }
       public AccountT Account { get; set; }
    }
}
