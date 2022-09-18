using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class DayWorkingTimeService : IDayWorkingTimeService
    {
        private readonly IRepository<DayWorkingTimeT> repo;

        static List<DayWorkingTimeT> WorkingTimeList { get; set; }

        public DayWorkingTimeService(IRepository<DayWorkingTimeT> repo)
        {
            this.repo = repo;
        }

        public async Task<List<DayWorkingTimeT>> GetListAsync()
        {
            if(WorkingTimeList != null)
            {
                return WorkingTimeList;
            }
            else
            {
                var list = await repo.GetListAsync();
                WorkingTimeList = list;
                return list;
            }
        }

        public async Task<List<ReservationAvailableTimeDto>> GetReservationAvailableTimes(int departmentId, DateTime selectedDate)
        {
            List<ReservationAvailableTimeDto> reservationAvailableTimeList = new List<ReservationAvailableTimeDto>();
            TimeSpan currentTime;
            var list = await GetListAsync();
            var dayOfWeek = selectedDate.DayOfWeek;
            var dayWorkTime = list.FirstOrDefault(d => d.DayNameInEnglish.ToLower() == dayOfWeek.ToString().ToLower());
            if (selectedDate.Date > DateTime.Now.Date)
            {
                currentTime = dayWorkTime.StartTime;
            }
            else
            {
                currentTime = DateTime.Now.AddHours(1).TimeOfDay;
            }
            while (currentTime < dayWorkTime.EndTime)
            {
                var reservationAvailableTimeDto = new ReservationAvailableTimeDto
                {
                    Time = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, currentTime.Hours, currentTime.Minutes, 0),
                    Caption = $"من {currentTime.Hours} إلى {currentTime.Hours + 1}"
                };
                currentTime = currentTime.Add(new TimeSpan(1, 0, 0));
                reservationAvailableTimeList.Add(reservationAvailableTimeDto);
            }
            return reservationAvailableTimeList;
        }
    }
}
