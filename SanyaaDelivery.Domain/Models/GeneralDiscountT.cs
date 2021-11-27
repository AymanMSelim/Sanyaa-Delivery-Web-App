using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class GeneralDiscountT
    {
        public GeneralDiscountT()
        {
            RequestDiscountT = new HashSet<RequestDiscountT>();
        }

        public int DiscountId { get; set; }
        public int DiscountTypeId { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountRefernceId { get; set; }
        public DateTime DiscountValidDateFrom { get; set; }
        public DateTime DiscountValidDateTo { get; set; }
        public double DiscountCompanyPercantage { get; set; }
        public int SystemUserId { get; set; }
        public double DiscountEmployeePercantage { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreationDate { get; set; }

        public DiscountTypeT DiscountType { get; set; }
        public SystemUserT SystemUser { get; set; }
        public ICollection<RequestDiscountT> RequestDiscountT { get; set; }
    }
}
