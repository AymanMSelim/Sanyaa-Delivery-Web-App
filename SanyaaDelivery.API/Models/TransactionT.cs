using System;
using System.Collections.Generic;

namespace SanyaaDelivery.API.Models
{
    public partial class TransactionT
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int? Amount { get; set; }
        public sbyte? ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public bool? IsCanceled { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? SystemUserId { get; set; }

        public SystemUserT SystemUser { get; set; }
    }
}
