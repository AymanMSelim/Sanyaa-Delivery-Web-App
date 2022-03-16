using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ServiceT
    {
        public ServiceT()
        {
            RequestServicesT = new HashSet<RequestServicesT>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int? DepartmentId { get; set; }
        public short ServiceCost { get; set; }
        public float ServiceDuration { get; set; }
        public string ServiceDes { get; set; }
        public short? ServiceDiscount { get; set; }
        public sbyte? ServicePoint { get; set; }
        public short? MaterialCost { get; set; }

        public DepartmentSub1T Department { get; set; }
        public ICollection<RequestServicesT> RequestServicesT { get; set; }
    }
}
