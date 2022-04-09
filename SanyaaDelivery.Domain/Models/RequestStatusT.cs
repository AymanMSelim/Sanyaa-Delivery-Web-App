using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestStatusT
    {
        public sbyte RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
        public string RequestStatusDes { get; set; }
    }
}
