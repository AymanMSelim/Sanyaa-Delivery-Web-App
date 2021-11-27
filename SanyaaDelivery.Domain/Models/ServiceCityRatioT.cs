using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class ServiceCityRatioT
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CityId { get; set; }
        public double Ratio { get; set; }
        public DateTime? DateForm { get; set; }
        public DateTime? DataTo { get; set; }
        public bool? IsActive { get; set; }
        public int SystemUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public AddressCityT City { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
