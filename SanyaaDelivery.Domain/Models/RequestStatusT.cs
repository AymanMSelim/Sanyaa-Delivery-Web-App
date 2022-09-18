using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestStatusT
    {
        public RequestStatusT()
        {
            RequestT = new HashSet<RequestT>();
        }

        public sbyte RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
        public string RequestStatusDes { get; set; }

        public ICollection<RequestT> RequestT { get; set; }
    }
}
