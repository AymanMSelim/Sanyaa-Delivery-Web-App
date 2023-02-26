using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class OpeningSoonDepartmentT
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? CityId { get; set; }

        public CityT City { get; set; }
        public DepartmentT Department { get; set; }
    }
}
