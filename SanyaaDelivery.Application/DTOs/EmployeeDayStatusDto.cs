using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.DTOs
{
    public class EmployeeDayStatusDto
    {
        public string EmployeeId { get; set; }
        public DateTime DateOfDay { get; set; }
        public string Status { get; set; }
        public List<OrderDto> OrdersList { get; set; }

        public EmployeeDayStatusDto(string employeeId, DateTime dateOfDay)
        {
            EmployeeId = employeeId;
            DateOfDay = dateOfDay;
        }
    }
}
