using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IDayWorkingTimeService
    {
         Task<List<DayWorkingTimeT>> GetListAsync();
         Task<List<ReservationAvailableTimeDto>> GetReservationAvailableTimes(int departmentId, DateTime selectedDate);
    }
}
