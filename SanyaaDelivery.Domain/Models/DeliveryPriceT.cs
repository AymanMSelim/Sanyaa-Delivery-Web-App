using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class DeliveryPriceT
    {
        public int DeliveryPriceId { get; set; }
        public int WorkingAreaId { get; set; }
        public int DepartmentId { get; set; }
        public int PriceValue { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        public int SystemUserId { get; set; }
        public DateTime CreationDate { get; set; }

        public SystemUserT SystemUser { get; set; }
        public WorkingAreaT WorkingArea { get; set; }
    }
}
