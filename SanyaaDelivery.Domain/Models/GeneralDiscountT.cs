using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class GeneralDiscountT
    {
        public int DiscountId { get; set; }
        public sbyte DiscountType { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountAppliedTo { get; set; }
        public sbyte IsActive { get; set; }
        public int? DepartmentId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime DiscountValidFrom { get; set; }
        public DateTime DiscountValidTo { get; set; }
        public int SystemUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string DiscountTarget { get; set; }

        public ServiceT Service { get; set; }
        public SystemUserT SystemUser { get; set; }
    }
}
