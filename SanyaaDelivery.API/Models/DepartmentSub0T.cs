using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class DepartmentSub0T
    {
        public DepartmentSub0T()
        {
            DepartmentSub1T = new HashSet<DepartmentSub1T>();
            LandingScreenItemDetailsT = new HashSet<LandingScreenItemDetailsT>();
        }

        public string DepartmentName { get; set; }
        public string DepartmentSub0 { get; set; }
        public int DepartmentSub0Id { get; set; }
        public int? DepartmentId { get; set; }

        public DepartmentT Department { get; set; }
        public ICollection<DepartmentSub1T> DepartmentSub1T { get; set; }
        public ICollection<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; }
    }
}
