using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class EmployeeWorkplacesT
    {
        public int EmployeeWorkplaceId { get; set; }

        public string EmployeeId { get; set; }

        public int BranchId { get; set; }

        public BranchT Branch { get; set; }

        public EmployeeT Employee { get; set; }

    }
}
