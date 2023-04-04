using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain.DTOs;
using AutoMapper;

namespace SanyaaDelivery.API.Controllers
{

    public class SubscriptionController : APIBaseAuthorizeController
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IClientSubscriptionService clientSubscriptionService;
        private readonly CommonService commonService;
        private readonly IDayWorkingTimeService dayWorkingTimeService;
        private readonly IMapper mapper;

        public SubscriptionController(ISubscriptionService subscriptionService,
            IClientSubscriptionService clientSubscriptionService, CommonService commonService,
            IDayWorkingTimeService dayWorkingTimeService, IMapper mapper)
        {
            this.subscriptionService = subscriptionService;
            this.clientSubscriptionService = clientSubscriptionService;
            this.commonService = commonService;
            this.dayWorkingTimeService = dayWorkingTimeService;
            this.mapper = mapper;
        }

        [HttpPost("Subscripe")]
        public async Task<ActionResult<Result<ClientSubscriptionT>>> Subscripe(ClientSubscriptionT clientSubscription)
        {
            try
            {
                var clientId = commonService.GetClientId(clientSubscription.ClientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.ReturnClientError());
                }
                var client = await commonService.GetClient(clientSubscription.ClientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                clientSubscription.SystemUserId = commonService.GetSystemUserId();
                clientSubscription.ClientId = clientId.Value;
                var result = await clientSubscriptionService.AddAsync(clientSubscription);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientSubscriptionT>(ex));
            }
        }


        [HttpPost("AddSubscription")]
        public async Task<ActionResult<Result<SubscriptionT>>> AddSubscription(SubscriptionT subscription)
        {
            try
            {
                var affectedRows = await subscriptionService.AddAsync(subscription);
                return Ok(ResultFactory<SubscriptionT>.CreateAffectedRowsResult(affectedRows, data: subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionT>(ex));
            }
        }

        [HttpPost("UpdateSubscription")]
        public async Task<ActionResult<Result<SubscriptionT>>> UpdateSubscription(SubscriptionT subscription)
        {
            try
            {
                var affectedRows = await subscriptionService.UpdateAsync(subscription);
                return Ok(ResultFactory<SubscriptionT>.CreateAffectedRowsResult(affectedRows, data: subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionT>(ex));
            }
        }

        [HttpGet("GetSubscription/{subscriptionId}")]
        public async Task<ActionResult<Result<SubscriptionT>>> GetSubscription(int subscriptionId)
        {
            try
            {
                var subscription = await subscriptionService.GetAsync(subscriptionId);
                return Ok(ResultFactory<SubscriptionT>.CreateSuccessResponse(subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionT>(ex));
            }
        }

        [HttpPost("DeleteSubscription/{subscriptionId}")]
        public async Task<ActionResult<Result<SubscriptionT>>> DeleteSubscription(int subscriptionId)
        {
            try
            {
                var affectedRows = await subscriptionService.DeletetAsync(subscriptionId);
                return Ok(ResultFactory<SubscriptionT>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionT>(ex));
            }
        }

        [HttpPost("AddSubscriptionService")]
        public async Task<ActionResult<Result<SubscriptionServiceT>>> AddSubscriptionService(SubscriptionServiceT subscriptionServiceT)
        {
            try
            {
                var affectedRows = await subscriptionService.AddSubscriptionServiceAsync(subscriptionServiceT);
                return Ok(ResultFactory<SubscriptionServiceT>.CreateAffectedRowsResult(affectedRows, data: subscriptionServiceT));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionServiceT>(ex));
            }
        }

        [HttpPost("UpdateSubscriptionService")]
        public async Task<ActionResult<Result<SubscriptionServiceT>>> UpdateSubscriptionService(SubscriptionServiceT subscriptionServiceT)
        {
            try
            {
                var affectedRows = await subscriptionService.UpdateSubscriptionServiceAsync(subscriptionServiceT);
                return Ok(ResultFactory<SubscriptionServiceT>.CreateAffectedRowsResult(affectedRows, data: subscriptionServiceT));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionServiceT>(ex));
            }
        }

        [HttpGet("GetSubscriptionService/{subscriptionServiceId}")]
        public async Task<ActionResult<Result<SubscriptionServiceT>>> GetSubscriptionService(int subscriptionServiceId)
        {
            try
            {
                var subscriptionServiceT = await subscriptionService.GetSubscriptionServiceAsync(subscriptionServiceId);
                return Ok(ResultFactory<SubscriptionServiceT>.CreateSuccessResponse(subscriptionServiceT));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionServiceT>(ex));
            }
        }

        [HttpPost("DeleteSubscriptionService/{subscriptionServiceId}")]
        public async Task<ActionResult<Result<SubscriptionServiceT>>> DeleteSubscriptionService(int subscriptionServiceId)
        {
            try
            {
                var affectedRows = await subscriptionService.DeletetSubscriptionServiceAsync(subscriptionServiceId);
                return Ok(ResultFactory<SubscriptionServiceT>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionServiceT>(ex));
            }
        }

        [HttpPost("AddSequence")]
        public async Task<ActionResult<Result<SubscriptionSequenceT>>> AddSequence(SubscriptionSequenceT subscriptionSequence)
        {
            try
            {
                var affectedRows = await subscriptionService.AddSequenceAsync(subscriptionSequence);
                return Ok(ResultFactory<SubscriptionSequenceT>.CreateAffectedRowsResult(affectedRows, data: subscriptionSequence));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionSequenceT>(ex));
            }
        }

        [HttpPost("UpdateSequence")]
        public async Task<ActionResult<Result<SubscriptionSequenceT>>> UpdateSequence(SubscriptionSequenceT subscriptionSequence)
        {
            try
            {
                var affectedRows = await subscriptionService.UpdateSequenceAsync(subscriptionSequence);
                return Ok(ResultFactory<SubscriptionSequenceT>.CreateAffectedRowsResult(affectedRows, data: subscriptionSequence));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionSequenceT>(ex));
            }
        }

        [HttpGet("GetSequence/{sequenceId}")]
        public async Task<ActionResult<Result<SubscriptionSequenceT>>> GetSequence(int sequenceId)
        {
            try
            {
                var subscription = await subscriptionService.GetSequenceAsync(sequenceId);
                return Ok(ResultFactory<SubscriptionSequenceT>.CreateSuccessResponse(subscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionSequenceT>(ex));
            }
        }

        [HttpGet("GetSequenceList/{subscriptionServiceId}")]
        public async Task<ActionResult<Result<List<SubscriptionSequenceT>>>> GetSequenceList(int subscriptionServiceId)
        {
            try
            {
                var subscriptionList = await subscriptionService.GetSequenceListAsync(subscriptionServiceId);
                return Ok(ResultFactory<List<SubscriptionSequenceT>>.CreateSuccessResponse(subscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<SubscriptionSequenceT>>(ex));
            }
        }

        [HttpPost("DeleteSequence/{sequenceId}")]
        public async Task<ActionResult<Result<SubscriptionSequenceT>>> DeleteSequence(int sequenceId)
        {
            try
            {
                var affectedRows = await subscriptionService.DeletetSequenceAsync(sequenceId);
                return Ok(ResultFactory<SubscriptionSequenceT>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<SubscriptionSequenceT>(ex));
            }
        }

        [HttpPost("UnSubscripe")]
        public async Task<ActionResult<Result<ClientSubscriptionT>>> UnSubscripe(int clientSubscriptionId)
        {
            try
            {
                var affectedRow = await clientSubscriptionService.UnSubscripe(clientSubscriptionId, commonService.GetSystemUserId());
                return Ok(ResultFactory<ClientSubscriptionT>.CreateAffectedRowsResult(affectedRow));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientSubscriptionT>(ex));
            }
        }

        [HttpGet("GetClientSubcriptionList")]
        public async Task<ActionResult<Result<List<ClientSubscriptionDto>>>> GetClientSubcriptionList(int? clientId = null, int? departmentId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.ReturnClientError());
                }
                var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId, true, true, true, true, true, true);
                var mapList = mapper.Map<List<ClientSubscriptionDto>>(clientSubscriptionList);
                foreach (var item in mapList)
                {
                    item.ShowCalenderButton = true;
                    item.ShowDetailsButton = false;
                    item.ShowEmployeeCalenderButton = true;
                    item.ShowFavouriteEmployeeButton = true;
                }
                return Ok(ResultFactory<List<ClientSubscriptionDto>>.CreateSuccessResponse(mapList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ClientSubscriptionDto>>(ex));
            }
        }

        [HttpGet("GetClientSubcription")]
        public async Task<ActionResult<Result<List<ClientSubscriptionT>>>> GetClientSubcription(int? clientId = null, int? departmentId = null)
        {
            try
            {
                clientId = commonService.GetClientId();
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.ReturnClientError());
                }
                var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId);
                return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateSuccessResponse(clientSubscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ClientSubscriptionT>>(ex));
            }
        }
        [HttpGet("GetSubscriptionList/{departmentId?}")]
        public async Task<ActionResult<Result<List<SubscriptionDto>>>> GetSubscriptionList(int? departmentId = null)
        {
            try
            {
                List<string> s = new List<string>();
                var subscriptionList = await subscriptionService.GetListAsync(departmentId, true, true);
                var mapList = mapper.Map<List<SubscriptionDto>>(subscriptionList);
                foreach (var item in mapList)
                {
                    item.Specification = item.Description.Split(";").ToList();
                }
                return Ok(ResultFactory<List<SubscriptionDto>>.CreateSuccessResponse(mapList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<SubscriptionDto>>(ex));
            }
        }

        [HttpGet("GetSubscriptionListByService/{serviceId}")]
        public async Task<ActionResult<Result<List<SubscriptionDto>>>> GetSubscriptionListByService(int serviceId)
        {
            try
            {
                List<string> s = new List<string>();
                s.Add("جدولة المواعيد بنفسك");
                s.Add("تحكم كامل فى زياراتك");
                var subscriptionList = await subscriptionService.GetListByServiceAsync(serviceId, true, true, true);
                var mapList = mapper.Map<List<SubscriptionDto>>(subscriptionList);
                foreach (var item in mapList)
                {
                    item.Specification = s;
                    s.Insert(0, item.DepartmentName);
                }
                return Ok(ResultFactory<List<SubscriptionDto>>.CreateSuccessResponse(mapList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<SubscriptionDto>>(ex));
            }
        }

        [HttpGet("GetSubscriptionServiceList/{subscriptionId}")]
        public async Task<ActionResult<Result<List<SubscriptionServiceDto>>>> GetSubscriptionServiceList(int subscriptionId)
        {
            try
            {
                var subscriptionServiceList = await subscriptionService.GetSubscriptionServiceListAsync(subscriptionId, null, true);
                var mapList = mapper.Map<List<SubscriptionServiceDto>>(subscriptionServiceList);
                return Ok(ResultFactory<List<SubscriptionServiceDto>>.CreateSuccessResponse(mapList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<SubscriptionServiceDto>>(ex));
            }
        }

        [HttpGet("GetInfo/{subscriptionServiceId}")]
        public async Task<ActionResult<Result<string>>> GetInfo(int subscriptionServiceId)
        {
            try
            {
                var subscriptionS = await subscriptionService.GetSubscriptionServiceAsync(subscriptionServiceId, true);
                return Ok(ResultFactory<string>.CreateSuccessResponse(subscriptionS.Subscription.Condition));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<string>(ex));
            }
        }

        [HttpGet("GetTimeList/{departmentId}")]
        public ActionResult<Result<List<ReservationAvailableTimeDto>>> GetTimeList(int departmentId)
        {
            try
            {
                var list = dayWorkingTimeService.GetTimeListForNewSubscription(departmentId);
                return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ReservationAvailableTimeDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetClientSubscription/{clientSubscriptionId}")]
        public async Task<ActionResult<Result<ClientSubscriptionT>>> GetClientSubscription(int clientSubscriptionId)
        {
            try
            {
                var clientSubscription = await clientSubscriptionService.GetAsync(clientSubscriptionId);
                return Ok(ResultFactory<ClientSubscriptionT>.CreateSuccessResponse(clientSubscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientSubscriptionT>(ex));
            }
        }

        [HttpPost("UpdateClientSubscription")]
        public async Task<ActionResult<Result<ClientSubscriptionT>>> UpdateClientSubscription(ClientSubscriptionT clientSubscription)
        {
            try
            {
                if (clientSubscription.SystemUserId == 0)
                {
                    clientSubscription.SystemUserId = commonService.GetSystemUserId();
                }
                var affectedRows = await clientSubscriptionService.UpdateAsync(clientSubscription);
                return Ok(ResultFactory<ClientSubscriptionT>.CreateAffectedRowsResult(affectedRows, data: clientSubscription));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientSubscriptionT>(ex));
            }
        }
    }
}