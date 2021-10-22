using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class OrderService : IOrderService
    {
        #region Order Status

        static List<int> CompleteStatus = new List<int>
        {
            14 ,20
        };

        static List<int> WaitingStatus = new List<int>
        {
            11, 12, 1
        };

        static List<int> InExcution = new List<int>
        {
            13
        };

        static List<int> Rejected = new List<int>
        {
            2
        };
        #endregion

        private readonly IRepository<RequestT> orderRepository;

        public OrderService(IRepository<RequestT> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task<RequestT> Get(int requestId)
        {
            return orderRepository.Get(requestId);
        }

        public Task<RequestT> GetDetails(int requestId)
        {
            return orderRepository.Where(r => r.RequestId == requestId)
                .Include("RequestServicesT")
                .Include("RequestStagesT").FirstOrDefaultAsync();
        }

        public Task<int> GetCanceledOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return orderRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && o.RequestCanceledT.Count > 0).CountAsync();
        }

        public Task<int> GetComlpeteOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return orderRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && CompleteStatus.Contains(o.RequestStatus)
              && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersList(string employeeId, DateTime time)
        {
            return orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(string employeeId, DateTime time)
        {
            return orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(DateTime day)
        {
            return orderRepository
               .Where(o => o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day
               && o.RequestCanceledT.Count == 0
               ).ToListAsync();
        }

        public Task<List<DayOrderDto>> GetDayOrdersCustom(DateTime day)
        {
            return orderRepository
               .Where(o => o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day
               ).Select(orderDay => new DayOrderDto
               {
                   Day = day,
                   RequestId = orderDay.RequestId,
                   ClientId = orderDay.ClientId,
                   EmployeeId = orderDay.EmployeeId,
                   RequestStatus = orderDay.RequestStatus,
                   ClientName = orderDay.Client.ClientName,
                   EmployeeName = orderDay.Employee.EmployeeName,
                   IsCanceled = orderDay.RequestCanceledT.Count == 0 ? false : true,
                   IsCleaningSubscriber = orderDay.Client.CleaningSubscribersT == null ? false : true
               }
               ).ToListAsync();
        }

        public Task<List<OrderDto>> GetEmployeeOrdersCustomList(string employeeId, DateTime day)
        {
            var data = orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day)
               .Select(d => new OrderDto
               {
                   OrderId = d.RequestId,
                   ClientId = d.ClientId,
                   ClientName =  d.Client.ClientName,
                   ClientPhone = d.Client.CurrentPhone, 
                   BranchName = d.Branch.BranchName,
                   OrderTime = d.RequestTimestamp,
                   OrderStatus = d.RequestStatus,
                   IsCanceled = d.RequestCanceledT.Count == 0 ? false : true,
                   OrderCost = d.RequestStagesT.Cost})
               .ToListAsync();
            return data;
        }

        public Task<int> GetAllOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return orderRepository
                .Where(o => o.EmployeeId == employeeId
                && o.RequestTimestamp.Value.Year == time.Year
                && o.RequestTimestamp.Value.Month == time.Month
                && o.RequestTimestamp.Value.Day == time.Day).CountAsync();
        }

        public Task<int> GetWaitingOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return orderRepository
                 .Where(o => o.EmployeeId == employeeId
                 && o.RequestTimestamp.Value.Year == time.Year
                 && o.RequestTimestamp.Value.Month == time.Month
                 && o.RequestTimestamp.Value.Day == time.Day
                 && WaitingStatus.Contains(o.RequestStatus)).CountAsync();
        }

        public Task<int> GetOrdersExceptCanceledCountByEmployee(string employeeId, DateTime time)
        {
            return orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<int> Add(RequestT request)
        {
            orderRepository.Insert(request);
            return orderRepository.Save();
        }
    }
}
