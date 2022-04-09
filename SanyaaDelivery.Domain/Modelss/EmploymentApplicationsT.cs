using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class EmploymentApplicationsT
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Department { get; set; }
        public string NationalId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public int? EmployeeBlockNum { get; set; }
        public int? EmployeeFlatNum { get; set; }
        public string EmployeeDes { get; set; }
        public string LocationText { get; set; }
        public double? LocationLatitude { get; set; }
        public double? LocationLangitude { get; set; }
        public string EmployeeRelativeName { get; set; }
        public string EmployeeRelativePhone { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
