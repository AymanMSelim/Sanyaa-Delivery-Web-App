using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class TimetableT
    {
        public string EmployeeId { get; set; }
        public DateTime TimetableDate { get; set; }
        public sbyte Timetable10 { get; set; }
        public sbyte Timetable1 { get; set; }
        public sbyte Timetable4 { get; set; }
        public sbyte Timetable7 { get; set; }
        public sbyte Timetable9 { get; set; }

        public EmployeeT Employee { get; set; }
    }
}
