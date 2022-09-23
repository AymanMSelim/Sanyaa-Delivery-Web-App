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

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public class RequestController : APIBaseAuthorizeController
    {
        private readonly IRequestService requestService;
        private readonly CommonService commonService;
        private readonly ICartService cartService;
        private readonly IClientService clientService;

        public RequestController(IRequestService requestService, CommonService commonService, ICartService cartService, IClientService clientService)
        {
            this.requestService = requestService;
            this.commonService = commonService;
            this.cartService = cartService;
            this.clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<RequestT>>> Add(RequestT request)
        {
            try
            {
                if (request.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.Add(request);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateSuccessResponse(request, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<RequestT>>> Add(AddRequestDto requestDto)
        {
            try
            {
                requestDto.ClientId = commonService.GetClientId(requestDto.ClientId);
                var cart = await commonService.GetClientCartAsync(requestDto.ClientId);
                var address = await clientService.GetAddress(requestDto.AddressId);
                var customCart = await cartService.GetCartForAppAsync(cart.CartId, address.CityId);
                var request = new RequestT
                {
                    RequestedAddressId = requestDto.AddressId,
                    RequestedPhoneId = requestDto.PhoneId,
                    ClientId = requestDto.ClientId.Value,
                    DepartmentId = cart.DepartmentId,
                    EmployeeId = requestDto.EmployeeId,
                    RequestNote = cart.Note,
                    SystemUserId = commonService.GetSystemUserId(),
                    RequestTimestamp = requestDto.RequestTime,
                    RequestCurrentTimestamp = DateTime.Now,
                    BillNumberT = null
                };
                return Ok(OpreationResultMessageFactory<RequestT>.CreateSuccessResponse(request, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                if (request.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.Add(request);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateSuccessResponse(request, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RequestT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{requestId}")]
        public async Task<ActionResult<RequestT>> Get(int requestId)
        {
            RequestT request = await requestService.Get(requestId);
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

        [AllowAnonymous]
        [HttpGet("GetList")]
        public async Task<List<RequestT>> GetList(DateTime? startDate, DateTime? endDate, int? requestId, int? clientId, string employeeId, int? systemUserId, int? requestStatus, bool? getCanceled)
        {
            return await requestService.GetList(startDate, endDate, requestId, clientId, employeeId, systemUserId, requestStatus, getCanceled);
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
