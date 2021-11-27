using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Application;
using App.Global.DTOs;

namespace SanyaaDelivery.API.Controllers
{
    public class AccountController : API.Controllers.APIBaseAuthorizeController
    {
        private readonly ISystemUserService systemUserService;
        private readonly ITokenService tokenService;
        private readonly IAccountService accountService;
        private readonly IAccountRoleService accountRoleService;
        private readonly IClientService clientService;
        private readonly IEmployeeService employeeService;
        private readonly IGeneralSetting generalSetting;
        private readonly IRegisterService registerService;

        public AccountController(ISystemUserService systemUserService, ITokenService tokenService,
            IAccountService accountService, IAccountRoleService accountRoleService,
            IClientService clientService, IEmployeeService employeeService, IGeneralSetting generalSetting,
            IRegisterService registerService)
        {
            this.systemUserService = systemUserService;
            this.tokenService = tokenService;
            this.accountService = accountService;
            this.accountRoleService = accountRoleService;
            this.clientService = clientService;
            this.employeeService = employeeService;
            this.generalSetting = generalSetting;
            this.registerService = registerService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<HttpResponseDto<object>>> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "Empty username or password", StatusDescreption = "Failed" });
            }

            string userId = null;
            object userData = null;
            if (user.AccountType == 1)
            {
                var userModel = await systemUserService.Get(user.Username);
                if (userModel != null)
                {
                    userId = userModel.SystemUserId.ToString();
                    userData = userModel;
                }
            }
            else if (user.AccountType == 2)
            {
                var employeeModel = await employeeService.Get(user.Username);
                if (employeeModel != null)
                {
                    userId = employeeModel.EmployeeId;
                    userData = employeeModel;
                }
            }
            else if (user.AccountType == 3)
            {
                var clientModel = await clientService.GetByPhone(user.Username);
                if (clientModel != null)
                {
                    userId = clientModel.ClientId.ToString();
                    userData = clientModel;
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var account = await accountService.Get(user.AccountType.Value, userId);
                if (account == null)
                    return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "No account for this user", StatusDescreption = "Failed" });

                if (account.IsActive == false)
                    return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "this account is suspend", StatusDescreption = "Failed" });


                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                    return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "No roles for this user", StatusDescreption = "Failed" });

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, user.Password);
                if (account.AccountPassword != password)
                {
                    return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "Invalid username or password", StatusDescreption = "Failed" });
                }
                return Ok(
                    new HttpResponseDto<SystemUserDto>
                    {
                        StatusCode = 1,
                        Message = "Token Generated Sussessfuly",
                        StatusDescreption = "Success",
                        Data = new SystemUserDto
                        {
                            Username = user.Username,
                            Token = tokenService.CreateToken(userId, user.Username, accountRoles),
                            TokenExpireDate = DateTime.Now.AddDays(1),
                            UserData = userData
                        }
                    });
            }
            else
            {
                return BadRequest(new HttpResponseDto<object> { StatusCode = 0, Message = "Invalid username or password", StatusDescreption = "Failed" });
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginClient")]
        public async Task<ActionResult<HttpResponseDto<ClientT>>> LoginClient(ClientLoginDto clientLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Empty username or password", App.Global.Eumns.ResponseStatusCode.EmptyData));
            }

            var client = await clientService.GetByPhone(clientLoginDto.Username);
            if (client == null)
            {
                return NotFound(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("This client not found", App.Global.Eumns.ResponseStatusCode.NotFound));
            }

            var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
            if (account == null)
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("No account for this client", App.Global.Eumns.ResponseStatusCode.NotFound));

            if (account.IsMobileVerfied == false)
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponse(new ClientRegisterResponseDto { Client = client, OtpCode =account.MobileOtpCode, SecurityCode = account.AccountSecurityCode, OTPExpireTime = account.LastOtpCreationTime.Value},  App.Global.Eumns.ResponseStatusCode.MobileVerificationRequired, "This account not verified"));

            if (account.IsActive == false)
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("This account is suspended", App.Global.Eumns.ResponseStatusCode.AccountSuspended));

            var accountRoles = await accountRoleService.GetList(account.AccountId, true);
            if (accountRoles == null)
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("No roles for this user", App.Global.Eumns.ResponseStatusCode.NoRoleFound));

            var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, clientLoginDto.Password);
            if (account.AccountPassword != password)
            {
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("Invalid username or password", App.Global.Eumns.ResponseStatusCode.InvalidUserOrPassword));
            }
            return Ok(
                HttpResponseDtoFactory<SystemUserDto>.CreateSuccessResponse(
                     new SystemUserDto
                     {
                         Username = clientLoginDto.Username,
                         Token = tokenService.CreateToken(client.ClientId.ToString(), clientLoginDto.Username, accountRoles),
                         TokenExpireDate = DateTime.Now.AddDays(1),
                         UserData = client
                     }
                    ));
        }


        [AllowAnonymous]
        [HttpPost("RegisterClient")]
        public async Task<ActionResult<HttpResponseDto<ClientRegisterResponseDto>>> RegisterClient(ClientRegisterDto clientRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(HttpResponseDtoFactory<ClientRegisterResponseDto>.CreateErrorResponseMessage("Empty username or password", App.Global.Eumns.ResponseStatusCode.EmptyData));
                }

                var clientExist = await clientService.GetByPhone(clientRegisterDto.Phone);
                if (clientExist != null)
                {
                    var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientRegisterDto.Phone);
                    return Ok(HttpResponseDtoFactory<ClientRegisterResponseDto>.CreateSuccessResponse(
                        new ClientRegisterResponseDto 
                        { 
                            Client = clientExist,
                            OtpCode = account.MobileOtpCode,
                            OTPExpireTime = account.LastOtpCreationTime.Value,
                            SecurityCode = account.AccountSecurityCode
                        }
                        ,App.Global.Eumns.ResponseStatusCode.AlreadyExist, "This phone is already exist"));
                }

                ClientRegisterResponseDto clientRegisterResponseDto = await registerService.RegisterClient(clientRegisterDto);
                if (clientRegisterResponseDto == null)
                {
                    return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
                }

                await App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientRegisterDto.Phone, $"Your confirmation OTP code is {clientRegisterResponseDto.OtpCode}");
                return Created($"https://{HttpContext.Request.Host}/api/client/get/{clientRegisterResponseDto.Client.ClientId}",
                    HttpResponseDtoFactory<ClientRegisterResponseDto>.CreateSuccessResponse(
                        clientRegisterResponseDto,
                        App.Global.Eumns.ResponseStatusCode.ClientRegisterdSuccessfully)
                    );
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
            }

        }

        [AllowAnonymous]
        [HttpPost("ConfirmOTPCode")]
        public async Task<ActionResult<HttpResponseDto<ClientT>>> ConfirmOTPCode(int? clientId, string phone, string otpCode, string signature)
        {
            if (!clientId.HasValue || string.IsNullOrEmpty(otpCode) || string.IsNullOrEmpty(signature))
            {
                return NotFound(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Empty data", App.Global.Eumns.ResponseStatusCode.EmptyData));
            }
            var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
            if (account == null)
            {
                return NotFound(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("This client not found", App.Global.Eumns.ResponseStatusCode.NotFound));
            }
            if (!HttpContext.User.Identity.IsAuthenticated && App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + otpCode + account.AccountSecurityCode).ToLower() != signature.ToLower())
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature));
            }
            if (account.MobileOtpCode != otpCode)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("OTP code is invalid", App.Global.Eumns.ResponseStatusCode.InvalidOTP));
            }
            if (account.LastOtpCreationTime.Value.AddMilliseconds(GeneralSetting.OTPExpireMinutes) > DateTime.Now)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("OTP code is expired", App.Global.Eumns.ResponseStatusCode.OTPExpired));
            }
            account.IsMobileVerfied = true;
            account.IsActive = true;
            int affectedRecord = await accountService.Update(account);
            if (affectedRecord < 0)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
            }
            return Ok(HttpResponseDtoFactory<ClientT>.CreateSuccessResponse());
        }

        [AllowAnonymous]
        [HttpPost("ResendOTPCode")]
        public async Task<ActionResult<HttpResponseDto<object>>> ResendOTPCode(int? clientId, string phone, string signature)
        {
            if (!clientId.HasValue || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(signature))
            {
                return NotFound(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("Empty data", App.Global.Eumns.ResponseStatusCode.EmptyData));
            }

            var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
            if (account == null)
            {
                return NotFound(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("This client not found", App.Global.Eumns.ResponseStatusCode.NotFound));
            }

            
            if (!HttpContext.User.Identity.IsAuthenticated && App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + account.AccountSecurityCode).ToLower() != signature.ToLower())
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature));
            }

            account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
            account.LastOtpCreationTime = DateTime.Now;
            await App.Global.SMS.SMSMisrService.SendSmsAsync("2", phone, $"Your confirmation OTP code is {account.MobileOtpCode}");
            int affectedRecord = await accountService.Update(account);
            if (affectedRecord < 0)
            {
                return BadRequest(HttpResponseDtoFactory<object>.CreateErrorResponse());
            }
            return Ok(HttpResponseDtoFactory<object>.CreateSuccessResponse(new 
            { 
                OTPCode = account.MobileOtpCode ,
                OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
            }));
        }

    }
}
