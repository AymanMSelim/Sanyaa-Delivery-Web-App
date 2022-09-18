using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentEmployeeT
    {
        public string EmployeeId { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentEmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public sbyte? Percentage { get; set; }

        public DepartmentT Department { get; set; }
        public EmployeeT Employee { get; set; }
    }
}
