using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class ChangeRequestTimeDto
    {
        public int RequestId { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public bool SkipCheckEmployee { get; set; }
    }
}
