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
        #region Oreder Status

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

        public Task<RequestT> Get(int orderId)
        {
            return orderRepository.Get(orderId);
        }

        public Task<int> GetCanceledOrdersCount(string employeeId, DateTime time)
        {
            return orderRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && o.RequestCanceledT != null).CountAsync();
        }

        public Task<int> GetComlpeteOrdersCount(string employeeId, DateTime time)
        {
            return orderRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && CompleteStatus.Contains(o.RequestStatus)
              && o.RequestCanceledT == null).CountAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrders(string employeeId, DateTime time)
        {
            return orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day).ToListAsync();
        }


        public Task<List<OrderDto>> GetEmployeeOrdersCustom(string employeeId, DateTime day)
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
                   IsCanceled = d.RequestCanceledT == null ? false : true,
                   OrderCost = d.RequestStagesT.Cost})
               .ToListAsync();
            return data;
        }

        public Task<int> GetOrdersCount(string employeeId, DateTime time)
        {
            return orderRepository
                .Where(o => o.EmployeeId == employeeId
                && o.RequestTimestamp.Value.Year == time.Year
                && o.RequestTimestamp.Value.Month == time.Month
                && o.RequestTimestamp.Value.Day == time.Day).CountAsync();
               
        }

        public Task<int> GetWaitingOrdersCount(string employeeId, DateTime time)
        {
            return orderRepository
                 .Where(o => o.EmployeeId == employeeId
                 && o.RequestTimestamp.Value.Year == time.Year
                 && o.RequestTimestamp.Value.Month == time.Month
                 && o.RequestTimestamp.Value.Day == time.Day
                 && WaitingStatus.Contains(o.RequestStatus)).CountAsync();
        }
    }
}
