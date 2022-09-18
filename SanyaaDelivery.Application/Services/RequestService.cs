using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Domain.DTOs;
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
    public class RequestService : IRequestService
    {
        #region Order Status

        static readonly List<int> CompleteStatus = new List<int>
        {
            14 ,20
        };

        static readonly List<int> WaitingStatus = new List<int>
        {
            11, 12, 1
        };

        static readonly List<int> InExcution = new List<int>
        {
            13
        };

        static readonly List<int> Rejected = new List<int>
        {
            2
        };
        #endregion

        private readonly IRepository<RequestT> requestRepository;

        public RequestService(IRepository<RequestT> orderRepository)
        {
            this.requestRepository = orderRepository;
        }

        public Task<RequestT> Get(int requestId)
        {
            return requestRepository.GetAsync(requestId);
        }

        public Task<RequestT> GetDetails(int requestId)
        {
            return requestRepository.Where(r => r.RequestId == requestId)
                .Include("RequestServicesT")
                .Include("RequestStagesT").FirstOrDefaultAsync();
        }

        public Task<int> GetCanceledOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && o.RequestCanceledT.Count > 0).CountAsync();
        }

        public Task<int> GetComlpeteOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && CompleteStatus.Contains(o.RequestStatus)
              && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersList(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(DateTime day)
        {
            return requestRepository
               .Where(o => o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day
               && o.RequestCanceledT.Count == 0
               ).ToListAsync();
        }

        public Task<List<DayOrderDto>> GetDayOrdersCustom(DateTime day)
        {
            return requestRepository
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
                   IsCanceled = orderDay.RequestCanceledT.Count != 0,
                   IsCleaningSubscriber = orderDay.Client.Cleaningsubscribers != null
               }
               ).ToListAsync();
        }

        public Task<List<OrderDto>> GetEmployeeOrdersCustomList(string employeeId, DateTime day)
        {
            var data = requestRepository
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
                   IsCanceled = d.RequestCanceledT.Count != 0,
                   OrderCost = d.RequestStagesT.Cost})
               .ToListAsync();
            return data;
        }

        public Task<int> GetAllOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
                .Where(o => o.EmployeeId == employeeId
                && o.RequestTimestamp.Value.Year == time.Year
                && o.RequestTimestamp.Value.Month == time.Month
                && o.RequestTimestamp.Value.Day == time.Day).CountAsync();
        }

        public Task<int> GetWaitingOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
                 .Where(o => o.EmployeeId == employeeId
                 && o.RequestTimestamp.Value.Year == time.Year
                 && o.RequestTimestamp.Value.Month == time.Month
                 && o.RequestTimestamp.Value.Day == time.Day
                 && WaitingStatus.Contains(o.RequestStatus)).CountAsync();
        }

        public Task<int> GetOrdersExceptCanceledCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<List<RequestT>> GetList(DateTime? startDate, DateTime? endDate, int? requestId,
            int? clientId, string employeeId, int? systemUserId, int? requestStaus, bool? getCanceled,
            bool? getClientName = null, bool? getEmploeeName = null, bool? getDepartment = null, 
            bool? getService = null, bool? getPrice = null,
            bool? includeClient = null, bool? includeEmployee = null, int? branchId = null)
        {
            var data = requestRepository.DbSet.AsQueryable();
            if (startDate.HasValue)
            {
                data = data.Where(d => d.RequestTimestamp >= startDate);
            }
            if (endDate.HasValue)
            {
                data = data.Where(d => d.RequestTimestamp <= endDate);
            }
            if (requestId.HasValue)
            {
                data = data.Where(d => d.RequestId == requestId);
            }
            if (clientId.HasValue)
            {
                data = data.Where(d => d.ClientId == clientId) ;
            }
            if (systemUserId.HasValue)
            {
                data = data.Where(d => d.SystemUserId == systemUserId);
            }
            if (requestStaus.HasValue)
            {
                data = data.Where(d => d.RequestStatus == requestStaus);
            }
            if (getCanceled.HasValue)
            {
                data = data.Where(d => d.RequestCanceledT.Any() == getCanceled);
            }
            if (!string.IsNullOrEmpty(employeeId))
            {
                data = data.Where(d => d.EmployeeId == employeeId);
            }
            if (branchId.HasValue)
            {
                data = data.Where(d => d.BranchId == branchId.Value);
            }
            return data.ToListAsync();
        }

        public async Task<int> Add(RequestT request)
        {
            await requestRepository.AddAsync(request);
            return await requestRepository.SaveAsync();
        }

        public Task<List<RequestT>> GetList(DateTime? startDate, DateTime? endDate, int? requestId, int? clientId, string employeeId, int? systemUserId, int? requestStatus, bool? getCanceled, bool? getClientName = null, bool? getEmploeeName = null, bool? getDepartment = null, bool? getService = null, bool? getPrice = null, bool? includeClient = null, bool? includeEmployee = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<RequestT>> GetUnPaidAsync(string employeeId, bool ignoreRequestWithValidFawryRequest = false)
        {
            var query = requestRepository
                .Where(d => d.EmployeeId == employeeId && d.RequestStagesT.PaymentFlag == 0);
            if (ignoreRequestWithValidFawryRequest)
            {
                query = query
                    .Where(d => d.FawryChargeRequestT.Any(t => t.Charge.ChargeStatus == App.Global.Enums.FawryOrderStatus.UNPAID.ToString()));
            }
            query = query.Include(d => d.RequestStagesT);
            return query.ToListAsync();
        }
    }
}
