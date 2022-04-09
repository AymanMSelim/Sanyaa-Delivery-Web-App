using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentT
    {
        public DepartmentT()
        {
            CartT = new HashSet<CartT>();
            DepartmentEmployeeT = new HashSet<DepartmentEmployeeT>();
            DepartmentSub0T = new HashSet<DepartmentSub0T>();
            RequestT = new HashSet<RequestT>();
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDes { get; set; }
        public string DepartmentImage { get; set; }

        public ICollection<CartT> CartT { get; set; }
        public ICollection<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public ICollection<DepartmentSub0T> DepartmentSub0T { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
    }
}
