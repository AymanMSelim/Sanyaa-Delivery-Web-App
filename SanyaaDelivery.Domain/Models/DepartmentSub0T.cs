using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentSub0T
    {
        public DepartmentSub0T()
        {
            DepartmentSub1T = new HashSet<DepartmentSub1T>();
        }

        public string DepartmentName { get; set; }
        public string DepartmentSub0 { get; set; }

        public DepartmentT DepartmentNameNavigation { get; set; }
        public ICollection<DepartmentSub1T> DepartmentSub1T { get; set; }
    }
}
