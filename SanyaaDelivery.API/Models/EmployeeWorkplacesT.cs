using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class EmployeeWorkplacesT
    {
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
        public int EmployeeWorkplaceId { get; set; }

        public BranchT Branch { get; set; }
        public EmployeeT Employee { get; set; }
    }
}
