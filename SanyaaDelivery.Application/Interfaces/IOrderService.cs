using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IOrderService
    {
        int GetOrdersCount(string employeeId, DateTime time);

        RequestT Get(int orderId);

        List<RequestT> GetEmployeeOrders(string employeeId, DateTime time);

    }
}
