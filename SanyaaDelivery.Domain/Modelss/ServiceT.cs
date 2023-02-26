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
            SubscriptionServiceT = new HashSet<SubscriptionServiceT>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int DepartmentId { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal ServiceDuration { get; set; }
        public string ServiceDes { get; set; }
        public decimal? MaterialCost { get; set; }
        public int? ServicePoints { get; set; }
        public bool? NoDiscount { get; set; }
        public decimal? ServiceDiscount { get; set; }
        public int? DiscountServiceCount { get; set; }
        public decimal? CompanyDiscountPercentage { get; set; }

        public DepartmentSub1T Department { get; set; }
        public ICollection<CartDetailsT> CartDetailsT { get; set; }
        public ICollection<FavouriteServiceT> FavouriteServiceT { get; set; }
        public ICollection<RequestServicesT> RequestServicesT { get; set; }
        public ICollection<SubscriptionServiceT> SubscriptionServiceT { get; set; }
    }
}
