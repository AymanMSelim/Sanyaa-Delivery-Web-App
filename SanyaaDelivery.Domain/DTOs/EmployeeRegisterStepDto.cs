using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeRegisterStepDto
    {
        public string NationalId { get; set; }
        public int? AccountId { get; set; }
        public int NextStep { get; set; }
        public string NextStepDescription { get; set; }
    }
}
