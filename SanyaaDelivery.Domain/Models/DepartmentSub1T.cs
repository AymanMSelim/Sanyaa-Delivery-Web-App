using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentSub1T
    {
        public DepartmentSub1T()
        {
            ServiceT = new HashSet<ServiceT>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentSub0 { get; set; }
        public string DepartmentSub1 { get; set; }
        public string DepartmentDes { get; set; }

        public DepartmentSub0T Department { get; set; }
        public ICollection<ServiceT> ServiceT { get; set; }
    }
}
