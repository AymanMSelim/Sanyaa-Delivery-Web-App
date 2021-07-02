using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<RequestT> orderRepository;

        public OrderService(IRepository<RequestT> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public RequestT Get(int orderId)
        {
            return orderRepository.Get(orderId);
        }

        public List<RequestT> GetEmployeeOrders(string employeeId, DateTime time)
        {
            return orderRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day).ToList();
        }

        public int GetOrdersCount(string employeeId, DateTime time)
        {
            return orderRepository
                .Where(o => o.EmployeeId == employeeId
                && o.RequestTimestamp.Value.Year == time.Year
                && o.RequestTimestamp.Value.Month == time.Month
                && o.RequestTimestamp.Value.Day == time.Day).Count();
               
        }
    }
}
