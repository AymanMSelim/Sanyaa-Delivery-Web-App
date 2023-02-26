using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;

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

        public async Task<List<ReservationAvailableTimeDto>> GetReservationAvailableTimesForCart(int departmentId, DateTime selectedDate)
        {
            List<ReservationAvailableTimeDto> reservationAvailableTimeList = new List<ReservationAvailableTimeDto>();

            if (selectedDate.Date < DateTime.Now.EgyptTimeNow().Date)
            {
                return reservationAvailableTimeList;
            }
            DateTime start;
            DateTime end;
            DayWorkingTimeT dayWorkTime;
            var list = await GetListAsync();
            var dayOfWeek = selectedDate.DayOfWeek;
            dayWorkTime = list.FirstOrDefault(d => d.DayNameInEnglish.ToLower() == dayOfWeek.ToString().ToLower());
            if (departmentId == GeneralSetting.CleaningDepartmentId)
            {
                end = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 11, 0, 0);
            }
            else
            {
                end = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, dayWorkTime.EndTime.Hours, dayWorkTime.EndTime.Minutes, 0);
            }
            if(selectedDate.Date == DateTime.Now.EgyptTimeNow().Date)
            {
                start = DateTime.Now.EgyptTimeNow();
                start = start.AddHours(1);
                if(start.TimeOfDay < dayWorkTime.StartTime)
                {
                    start = new DateTime(start.Year, start.Month, start.Day, dayWorkTime.StartTime.Hours, dayWorkTime.StartTime.Minutes, 0);
                }
            }
            else
            {
                start = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, dayWorkTime.StartTime.Hours, dayWorkTime.StartTime.Minutes, 0);
            }
            reservationAvailableTimeList = GetTimeList(start, end);
            return reservationAvailableTimeList;
        }

        public List<ReservationAvailableTimeDto> GetTimeListForNewSubscription(int departmentId)
        {
            List<ReservationAvailableTimeDto> reservationAvailableTimeList = new List<ReservationAvailableTimeDto>();
            var selectedDate = DateTime.Now;
            DateTime start;
            DateTime end;
            if (departmentId == GeneralSetting.CleaningDepartmentId)
            {
                end = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 11, 0, 0);
            }
            else
            {
                end = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 21, 0, 0);
            }
            start = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 9, 0, 0);
            reservationAvailableTimeList = GetTimeList(start, end);
            return reservationAvailableTimeList;
        }

        private List<ReservationAvailableTimeDto> GetTimeList(DateTime start, DateTime end)
        {
            List<ReservationAvailableTimeDto> reservationAvailableTimeList = new List<ReservationAvailableTimeDto>();
            if(start.Hour >= 21)
            {
                return reservationAvailableTimeList;
            }
            while (start < end)
            {
                var reservationAvailableTimeDto = new ReservationAvailableTimeDto();
                reservationAvailableTimeDto.Time = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
                if (start.Hour == 12)
                {
                    reservationAvailableTimeDto.Caption = $"من 12 إلى 1 {App.Global.Translation.Translator.STranlate(start.Hour < 12 ? "AM" : "PM")}";
                }
                else
                {
                    if (start.Hour + 1 == 12)
                    {
                        reservationAvailableTimeDto.Caption = $"من 11 إلى 12 {App.Global.Translation.Translator.STranlate(start.Hour < 12 ? "AM" : "PM")}";
                    }
                    else
                    {
                        reservationAvailableTimeDto.Caption = $"من  {start.Hour % 12} إلى {(start.Hour + 1) % 12} {App.Global.Translation.Translator.STranlate(start.Hour < 12 ? "AM" : "PM")}";
                    }
                }
                start = start.AddHours(1);
                reservationAvailableTimeList.Add(reservationAvailableTimeDto);
            }
            return reservationAvailableTimeList;
        }
    }
}
