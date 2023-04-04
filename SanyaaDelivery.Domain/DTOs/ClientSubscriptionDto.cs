using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ClientSubscriptionDto
    {
        public int ClientSubscriptionId { get; set; }
        public int SubscriptionServiceId { get; set; }
        public int DepartmentId { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string ImageUrl { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public string VisitCountDescription { get; set; }
        public string Description { get; set; }
        public int? AddressId { get; set; }
        public string Address { get; set; }
        public int? PhoneId { get; set; }
        public string Phone { get; set; }
        public string VisitTime { get; set; }
        public bool ShowDetailsButton { get; set; }
        public bool ShowEmployeeCalenderButton { get; set; }
        public bool ShowCalenderButton { get; set; }
        public bool ShowFavouriteEmployeeButton { get; set; }
    }
}
