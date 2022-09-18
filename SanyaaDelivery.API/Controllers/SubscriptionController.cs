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
namespace SanyaaDelivery.API.Controllers
{
   
    public class SubscriptionController : APIBaseAuthorizeController
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly IAppLandingScreenService appLandingScreenService;
        private readonly IClientSubscriptionService clientSubscriptionService;
        private readonly CommonService commonService;

        public SubscriptionController(ISubscriptionService subscriptionService, IAppLandingScreenService appLandingScreenService, IClientSubscriptionService clientSubscriptionService, CommonService commonService)
        {
            this.subscriptionService = subscriptionService;
            this.appLandingScreenService = appLandingScreenService;
            this.clientSubscriptionService = clientSubscriptionService;
            this.commonService = commonService;
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentSubscription>>>> GetList(int? itemId = null, int? departmentId = null)
        {
            List<int> departmentIdList = null;
            try
            {
                if (itemId.HasValue)
                {
                    var itemList = await appLandingScreenService.GetDetailsItemListAsync(itemId.Value);
                    if (itemList.IsEmpty())
                    {
                        return Ok(OpreationResultMessageFactory<List<DepartmentSubscription>>.CreateSuccessResponse(null));
                    }
                    departmentIdList = itemList
                   .Select(d => d.DepartmentId).ToList();
                }
                else if(departmentId.HasValue)
                {
                    departmentIdList = new List<int> { departmentId.Value };
                }
                var subscriptionList = await subscriptionService.GetListAsync(departmentIdList, true, true);
                List<DepartmentSubscription> departmentSubscriptionList = subscriptionList.Select(d => new DepartmentSubscription(d)).ToList();
                departmentSubscriptionList.ForEach(d => d.Department = null);
                return Ok(OpreationResultMessageFactory<List<DepartmentSubscription>>.CreateSuccessResponse(departmentSubscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSubscription>>(ex));
            }
        }


        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentSubscription>>>> GetSubcriptionList(int? clientId = null, int? departmentId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientSubscriptionT>.CreateErrorResponseMessage("ClientId can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId, true, true);
                var subscriptionList = clientSubscriptionList.Select(d => d.Subscription).ToList();
                List<DepartmentSubscription> departmentSubscriptionList = subscriptionList.Select(d => new DepartmentSubscription(d)).ToList();
                departmentSubscriptionList.ForEach(d => d.Department = null);
                return Ok(OpreationResultMessageFactory<List<DepartmentSubscription>>.CreateSuccessResponse(departmentSubscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSubscription>>(ex));
            }
        }

    }
}
