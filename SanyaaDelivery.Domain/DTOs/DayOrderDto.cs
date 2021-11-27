using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class DayOrderDto
    {
        public DateTime Day { get; set; }
        public int RequestId { get; set; }
        public int RequestStatus { get; set; }
        public bool IsCanceled { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsCleaningSubscriber { get; set; }

    }
}
