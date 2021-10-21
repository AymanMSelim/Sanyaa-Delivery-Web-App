using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : APIBaseAuthorizeController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(RequestT request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { Message = "Request can't be null" });
                }
                int affectedRecords = await orderService.Add(request);
                if (affectedRecords > 0)
                {
                    return Created(Request.Path.Value, request);
                }
                else
                {
                    return BadRequest(new { Message = "Error happend while adding your request" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = "Exception happen" });
            }
        }

        [HttpGet("Get/{requestId}")]
        public async Task<ActionResult<RequestT>> Get(int requestId)
        {
            RequestT request = await orderService.Get(requestId);
            if (request != null)
            {
                return Ok(request);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetDetails/{requestId}")]
        public async Task<ActionResult<RequestT>> GetDetails(int requestId)
        {
            RequestT request = await orderService.GetDetails(requestId);
            if(request != null)
            {
                return Ok(request);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetEmployeeOrders")]
        public async Task<ActionResult<List<RequestT>>> GetEmployeeOrders(string employeeId, DateTime day)
        {
            var orders = await orderService.GetEmployeeOrdersList(employeeId, day);
            if(orders == null) { return NotFound(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetCustomOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetEmployeeOrdersCustom(string employeeId, DateTime day)
        {
            var orders = await orderService.GetEmployeeOrdersCustomList(employeeId, day);
            if (orders == null) { return NotFound(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetAllOrdersCount(string employeeId, DateTime day)
        {
            var orders = await orderService.GetAllOrdersCountByEmployee(employeeId, day);
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
            var orders = await orderService.GetWaitingOrdersCountByEmployee(employeeId, day);
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
            var orders = await orderService.GetComlpeteOrdersCountByEmployee(employeeId, day);
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
            var orders = await orderService.GetCanceledOrdersCountByEmployee(employeeId, day);
            return new OrderCountDto
            {
                Day = day.Date,
                EmployeeId = employeeId,
                OrderStatus = "Canceled",
                Count = orders
            };
        }

        [HttpGet("GetDayOrders")]
        public async Task<ActionResult<List<DayOrderDto>>> GetDayOrdersCustom(DateTime day, bool getCanceled = false)
        {
            var orders = await orderService.GetDayOrdersCustom(day);
            if (getCanceled)
            {
                return orders;
            }
            else
            {
                return orders.Where(d => d.IsCanceled == false).ToList();
            }
        }
    }
}
