using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class UpdatePreferredWorkingHourDto
    {
        public string EmployeeId { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
