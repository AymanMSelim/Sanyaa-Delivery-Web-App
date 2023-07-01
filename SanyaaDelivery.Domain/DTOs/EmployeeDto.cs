using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeePhone { get; set; }

        public string Address { get; set; }

        public string WorkPlace { get; set; } 

        public string Deparment { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
