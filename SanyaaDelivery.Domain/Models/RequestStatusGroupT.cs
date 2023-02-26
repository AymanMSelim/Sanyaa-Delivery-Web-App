using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestStatusGroupT
    {
        public RequestStatusGroupT()
        {
            RequestStatusT = new HashSet<RequestStatusT>();
        }

        public int RequestStatusGroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public ICollection<RequestStatusT> RequestStatusT { get; set; }
    }
}
