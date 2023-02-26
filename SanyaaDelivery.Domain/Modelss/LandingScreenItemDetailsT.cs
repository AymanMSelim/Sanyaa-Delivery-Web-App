using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class LandingScreenItemDetailsT
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public int? DapartmentSub0Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public DepartmentSub0T DapartmentSub0 { get; set; }
        public DepartmentT Department { get; set; }
        public AppLandingScreenItemT Item { get; set; }
    }
}
