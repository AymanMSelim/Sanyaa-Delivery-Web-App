using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.API.ActionsFilter;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain.OtherModels;
using AutoMapper;

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public class OrderController : APIBaseAuthorizeController
    {
        private readonly IRequestService requestService;
        private readonly CommonService commonService;
        private readonly ICartService cartService;
        private readonly IClientService clientService;
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        public OrderController(IRequestService requestService, 
            CommonService commonService, ICartService cartService, 
            IClientService clientService, ICityService cityService, IMapper mapper, CommonService commonService1) : base(commonService)
        {
            this.requestService = requestService;
            this.commonService = commonService;
            this.cartService = cartService;
            this.clientService = clientService;
            this.cityService = cityService;
            this.mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<RequestT>>> Add(RequestT request)
        {
            try
            {
                if (request.IsNull())
                {
                    return Ok(ResultFactory<RequestT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.AddAsync(request);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<RequestT>.CreateSuccessResponse(request, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<RequestT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{requestId}")]
        public async Task<ActionResult<RequestT>> Get(int requestId)
        {
            RequestT request = await requestService.GetAsync(requestId);
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
            RequestT request = await requestService.GetDetails(requestId);
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
            var orders = await requestService.GetEmployeeOrdersList(employeeId, day);
            if(orders == null) { return Ok(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetCustomOrders")]
        public async Task<ActionResult<List<OrderDto>>> GetEmployeeOrdersCustom(string employeeId, DateTime day)
        {
            var orders = await requestService.GetEmployeeOrdersCustomList(employeeId, day);
            if (orders == null) { return Ok(new { Message = "No orders matched" }); };
            return Ok(orders);
        }

        [HttpGet("GetOrderCount")]
        public async Task<ActionResult<OrderCountDto>> GetAllOrdersCount(string employeeId, DateTime day)
        {
            var orders = await requestService.GetAllOrdersCountByEmployee(employeeId, day);
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
            var orders = await requestService.GetWaitingOrdersCountByEmployee(employeeId, day);
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
            var orders = await requestService.GetComlpeteOrdersCountByEmployee(employeeId, day);
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
            var orders = await requestService.GetCanceledOrdersCountByEmployee(employeeId, day);
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
            var orders = await requestService.GetDayOrdersCustom(day);
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
