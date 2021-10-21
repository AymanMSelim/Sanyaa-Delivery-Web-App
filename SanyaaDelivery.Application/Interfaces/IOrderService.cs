using SanyaaDelivery.Application.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> GetAllOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetComlpeteOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetWaitingOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetCanceledOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetOrdersExceptCanceledCountByEmployee(string employeeId, DateTime time);
        Task<RequestT> Get(int requestId);
        Task<RequestT> GetDetails(int requestId);
        Task<List<RequestT>> GetEmployeeOrdersList(string employeeId, DateTime time);
        Task<List<OrderDto>> GetEmployeeOrdersCustomList(string employeeId, DateTime day);
        Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(DateTime day);
        Task<List<DayOrderDto>> GetDayOrdersCustom(DateTime day);
        Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(string employeeId, DateTime day);
        Task<int> Add(RequestT request);

    }


}
