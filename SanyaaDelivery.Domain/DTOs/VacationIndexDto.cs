using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Domain.DTOs
{
    public class VacationIndexDto
    {
        public bool OpenVacation { get; set; }
        public int? PreferredWorkingStartHour { get; set; }
        public int? PreferredWorkingEndHour { get; set; }
        public List<AppVacationDto> VacationList { get; set; }
    }

    public class AppVacationDto
    {
        public int? VacationId { get; set; }
        public DateTime Day { get; set; }
        public string DayCaption { get; set; }
        public bool IsVacation { get; set; }
    }


    public class SwitchVacationDto
    {
        public int? VacationId { get; set; }
        public DateTime Day { get; set; }
        public string EmployeeId { get; set; }
    }
}
