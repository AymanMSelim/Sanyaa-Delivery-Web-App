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
        Task<int> GetOrdersCount(string employeeId, DateTime time);

        Task<int> GetComlpeteOrdersCount(string employeeId, DateTime time);

        Task<int> GetWaitingOrdersCount(string employeeId, DateTime time);

        Task<int> GetCanceledOrdersCount(string employeeId, DateTime time);

        Task<RequestT> Get(int orderId);

        Task<List<RequestT>> GetEmployeeOrders(string employeeId, DateTime time);

        Task<List<OrderDto>> GetEmployeeOrdersCustom(string employeeId, DateTime day);

    }
}
