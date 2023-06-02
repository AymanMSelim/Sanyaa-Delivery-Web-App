using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class AppEmployeeAccountIndexDto
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeePhone1 { get; set; }
        public string EmployeeImage { get; set; }
        public string EmployeeWorkplaceText { get; set; }
        public string EmployeeDepartmenText { get; set; }
        public DateTime CreationTime { get; set; }
        public List<EmployeeWorkplacesDto> EmployeeWorkplaceList { get; set; }
        public List<EmployeeDepartmentDto> EmployeeDepartmentList { get; set; }
    }
}
