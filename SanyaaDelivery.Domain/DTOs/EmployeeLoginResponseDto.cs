using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeLoginResponseDto
    {
        public EmployeeT Employee { get; set; }
        public string SecurityCode { get; set; }
        public int NextRegisterStep { get; set; }
        public string NextRegisterStepDescription { get; set; }
        public string Token { get; set; }
        public bool IsDataComplete { get; set; }
        public int AccountId { get; set; }

        public string FCMToken { get; set; }
    }
}
