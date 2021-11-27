using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentT
    {
        public DepartmentT()
        {
            DepartmentEmployeeT = new HashSet<DepartmentEmployeeT>();
            DepartmentSub0T = new HashSet<DepartmentSub0T>();
        }

        public string DepartmentName { get; set; }
        public string DepartmentImage { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentDes { get; set; }

        public ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public ICollection<DepartmentSub0T> DepartmentSub0T { get; set; }
    }
}
