using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class PromocodeDepartmentT
    {
        public int PromocodeDepartmentJd { get; set; }
        public int? DepartmentId { get; set; }
        public int? PromocodeId { get; set; }

        public DepartmentT Department { get; set; }
        public PromocodeT Promocode { get; set; }
    }
}
