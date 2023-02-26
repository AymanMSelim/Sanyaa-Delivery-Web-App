using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class RequestCalendarDto
    {
        public int? RequestId { get; set; }
        public string RequestCaption { get; set; }
        public bool? IsCanceled { get; set; }
        public int Status { get; set; } // 0 free, 1 canceled, 2 filled
        public bool CanAdd { get; set; }
        public string Day { get; set; }
        public DateTime? PreferredTime { get; set; }
        public string DayOfTheWeek { get; set; }
    }
}
