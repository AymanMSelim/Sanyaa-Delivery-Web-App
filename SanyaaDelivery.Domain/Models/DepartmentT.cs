using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DepartmentT
    {
        public DepartmentT()
        {
            CartT = new HashSet<CartT>();
            RequestT = new HashSet<RequestT>();
            ServiceRatioDetailsT = new HashSet<ServiceRatioDetailsT>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDes { get; set; }
        public string DepartmentImage { get; set; }

        public ICollection<CartT> CartT { get; set; }
        public ICollection<RequestT> RequestT { get; set; }
        public ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
    }
}
