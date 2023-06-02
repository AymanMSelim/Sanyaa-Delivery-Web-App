using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class MessagesT
    {
        public string EmployeeId { get; set; }
        public DateTime MessageTimestamp { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public sbyte? IsRead { get; set; }
        public int Id { get; set; }

        public EmployeeT Employee { get; set; }
    }
}
