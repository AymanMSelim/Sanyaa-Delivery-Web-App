using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestAccountT
    {
        public string RequestAccountId { get; set; }
        public int RequestId { get; set; }
        public double RequestTotalCost { get; set; }
        public double? RequestDeliveryCost { get; set; }
        public double RequestEmpoyeeDiscount { get; set; }
        public double RequestCompanyDiscount { get; set; }
        public double RequestTotalDiscount { get; set; }
        public double RequestMaterialsCost { get; set; }
        public double RequestNetCost { get; set; }
        public double RequestEmployeePercentage { get; set; }
        public double RequestCompanyPercentage { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public int? ModificationSystemUserId { get; set; }

        public RequestT Request { get; set; }
    }
}
