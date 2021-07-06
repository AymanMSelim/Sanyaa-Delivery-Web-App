using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class OrderController : APIBaseAuthorizeController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetEmployeeOrders")]
        public async Task<List<RequestT>> GetEmployeeOrders(string employeeId, DateTime time)
        {
            var orders = await orderService.GetEmployeeOrders(employeeId, time);
            return orders;
        }
    }
}
