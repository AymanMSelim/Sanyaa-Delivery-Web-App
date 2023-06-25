using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DepartmentEmployeeT
{
    public string EmployeeId { get; set; }

    public string DepartmentName { get; set; }

    public int DepartmentEmployeeId { get; set; }

    public int? DepartmentId { get; set; }

    public sbyte? Percentage { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
