using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.DTOs
{
    public class EmployeeAppAccountDto
    {
        public string EmployeeId { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsActive { get; set; }

        public bool IsEnabled { get; set; }

        public string Message { get; set; }
    }
}
