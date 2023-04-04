using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class MessagingController : APIBaseAuthorizeController
    {
        private readonly IAccountService accountService;
        private readonly CommonService commonService;

        public MessagingController(IAccountService  accountService, CommonService commonService)
        {
            this.accountService = accountService;
            this.commonService = commonService;
        }
        [HttpPost("AddFirebaseToken")]
        public async Task<ActionResult<Result<object>>> AddFirebaseToken(AddFirebaseTokenDto addFirebaseTokenDto)
        {
            AccountT account = null;
            try
            {
                if (commonService.IsViaApp())
                {
                    var clientId = commonService.GetClientId();
                    account = await accountService.Get((int)SanyaaDelivery.Domain.Enum.AccountType.Client, clientId.ToString());
                }
                else
                {
                    if (addFirebaseTokenDto.AccountId.HasValue && addFirebaseTokenDto.AccountId != 0)
                    {
                        account = await accountService.Get(addFirebaseTokenDto.AccountId.Value);
                    }
                }
                if (account.IsNotNull())
                {
                    account.FcmToken = addFirebaseTokenDto.Token;
                    var affectedRows = await accountService.Update(account);
                    return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
                }
                return ResultFactory<object>.CreateErrorResponseMessage("Error update token");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SendFirebaseNotificationtToClient")]
        public async Task<ActionResult<Result<object>>> SendFirebaseNotificationtToClient(SendFirebaseNotificationDto model)
        {
            try
            {
                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, model.Id);
                if (account.IsNull() || string.IsNullOrEmpty(account.FcmToken))
                {
                    return ResultFactory<object>.CreateNotFoundResponse("No token for this client");
                }
                await App.Global.Firebase.FirebaseMessaging.Send(account.FcmToken, model.Title, model.Body);
                return ResultFactory<object>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }
    }
}
