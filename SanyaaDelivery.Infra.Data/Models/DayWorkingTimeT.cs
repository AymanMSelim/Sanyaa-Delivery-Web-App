using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class DayWorkingTimeT
{
    public int DayWorkingTimeId { get; set; }

    public string DayNameInEnglish { get; set; }

    public int DayOfTheWeekIndex { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}
