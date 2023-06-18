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
        private readonly INotificatonService notificatonService;

        public MessagingController(IAccountService accountService, CommonService commonService, INotificatonService notificatonService) : base(commonService)
        {
            this.accountService = accountService;
            this.commonService = commonService;
            this.notificatonService = notificatonService;
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
                await App.Global.Firebase.FirebaseMessaging.SendToClientAsync(account.FcmToken, model.Title, model.Body, model.ImageURL);
                return ResultFactory<object>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SendFirebaseNotificationtToEmployee")]
        public async Task<ActionResult<Result<object>>> SendFirebaseNotificationtToEmployee(SendFirebaseNotificationDto model)
        {
            try
            {
                var account = await accountService.Get(GeneralSetting.EmployeeAccountTypeId, model.Id);
                if (account.IsNull() || string.IsNullOrEmpty(account.FcmToken))
                {
                    return ResultFactory<object>.CreateNotFoundResponse("No token for this account");
                }
                await App.Global.Firebase.FirebaseMessaging.SendToEmpAsync(account.FcmToken, model.Title, model.Body, model.ImageURL);
                return ResultFactory<object>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SendFirebaseNotification")]
        public async Task<ActionResult<Result<AppNotificationT>>> SendFirebaseNotification(AppNotificationT model)
        {
            try
            {
                var result = await notificatonService.SendFirebaseNotificationAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetNotificationList/{accountId?}")]
        public async Task<ActionResult<Result<List<AppNotificationT>>>> GetNotificationList(int? accountId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    accountId = commonService.GetAccountId();
                }
                if (accountId.IsNull())
                {
                    return Ok(ResultFactory<List<AppNotificationT>>.ReturnAccountError());
                }
                var list = await notificatonService.GetListAsync(accountId.Value);
                if (list.HasItem())
                {
                    list.ForEach(d => d.Body = d.Body + $"\n {d.CreationTime}");
                }
                return Ok(ResultFactory<List<AppNotificationT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppNotificationT>>.CreateExceptionResponse(ex));
            }
        }

    }
}
