using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ServiceT
    {
        public ServiceT()
        {
            CartDetailsT = new HashSet<CartDetailsT>();
            FavouriteServiceT = new HashSet<FavouriteServiceT>();
            RequestServicesT = new HashSet<RequestServicesT>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int DepartmentId { get; set; }
        public short ServiceCost { get; set; }
        public float ServiceDuration { get; set; }
        public string ServiceDes { get; set; }
        public short? MaterialCost { get; set; }
        public sbyte? ServicePoints { get; set; }
        public bool? NoDiscount { get; set; }
        public short? ServiceDiscount { get; set; }
        public sbyte? DiscountServiceCount { get; set; }
        public sbyte? CompanyDiscountPercentage { get; set; }

        public DepartmentSub1T Department { get; set; }
        public ICollection<CartDetailsT> CartDetailsT { get; set; }
        public ICollection<FavouriteServiceT> FavouriteServiceT { get; set; }
        public ICollection<RequestServicesT> RequestServicesT { get; set; }
    }
}
