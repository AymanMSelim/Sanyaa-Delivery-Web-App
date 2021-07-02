using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentEmployeeT
    {
        public string EmployeeId { get; set; }
        public string DepartmentName { get; set; }

        public DepartmentT DepartmentNameNavigation { get; set; }
        public EmployeeT Employee { get; set; }
    }
}
