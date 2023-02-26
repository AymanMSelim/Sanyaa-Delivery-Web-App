using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class ServiceRatioDetailsT
    {
        public int ServiceRatioDetailsId { get; set; }
        public int? ServiceRatioId { get; set; }
        public int? CityId { get; set; }
        public int? DepartmentId { get; set; }

        public CityT City { get; set; }
        public DepartmentT Department { get; set; }
        public ServiceRatioT ServiceRatio { get; set; }
    }
}
