using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.API.ActionsFilter;
using SanyaaDelivery.Application.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    [CheckOrderCountParameterActionFilter]
    public class OrderController : APIBaseAuthorizeController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        
        [HttpGet("GetEmployeeOrders")]
        public async Task<ActionResult<List<RequestT>>> GetEmployeeOrders(string employeeId, DateTime day)
        {
            var orders = await orderService.GetEmployeeOrders(employeeId, day);
            if(orders == null) { return NotFound(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetCustomOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetEmployeeOrdersCustom(string employeeId, DateTime day)
        {
            var orders = await orderService.GetEmployeeOrdersCustom(employeeId, day);
            if (orders == null) { return NotFound(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetAllOrdersCount(string employeeId, DateTime day)
        {
            var orders = await orderService.GetOrdersCount(employeeId, day);
            return Ok(new OrderCountDto
            {
                Day = day.Date,
                EmployeeId = employeeId,
                OrderStatus = "AllOrders",
                Count = orders
            });
        }

        [HttpGet("GetWaitingOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetWaitingOrderCount(string employeeId, DateTime day)
        {
            var orders = await orderService.GetWaitingOrdersCount(employeeId, day);
            return Ok(new OrderCountDto
            {
                Day = day.Date,
                EmployeeId = employeeId,
                OrderStatus = "Waiting",
                Count = orders
            });
        }

        [HttpGet("GetCompleteOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetCompleteOrderCount(string employeeId, DateTime day)
        {
            var orders = await orderService.GetComlpeteOrdersCount(employeeId, day);
            return Ok(new OrderCountDto
            {
                Day = day.Date,
                EmployeeId = employeeId,
                OrderStatus = "Complete",
                Count = orders
            });
        }

        [HttpGet("GetCanceledOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetCanceledOrderCount(string employeeId, DateTime day)
        {
            var orders = await orderService.GetCanceledOrdersCount(employeeId, day);
            return new OrderCountDto
            {
                Day = day.Date,
                EmployeeId = employeeId,
                OrderStatus = "Canceled",
                Count = orders
            };
        }
    }
}
