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
        [HttpGet("GetToken")]
        public object GetToken()
        {
            return Ok(
                   new
                   {
                       Username = "AymanSelim",
                       Token = tokenService.CreateToken("1", "AymanSelim", new List<AccountRoleT>()),
                       TokenExpireDate = DateTime.Now.AddDays(1),
                   });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<HttpResponseDto<object>>> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                var response = HttpResponseDtoFactory<string>.CreateModelNotValidResponse("Empty username or password");
                return BadRequest(response);
            }

            string userId = null;
            object userData = null;
            if (user.AccountType == ((int)Domain.Enum.AccountType.User))
            {
                var userModel = await systemUserService.Get(user.Username);
                if (userModel != null)
                {
                    userId = userModel.SystemUserId.ToString();
                    userData = userModel;
                }
            }
            else if (user.AccountType == ((int)Domain.Enum.AccountType.Employee))
            {
                var employeeModel = await employeeService.Get(user.Username);
                if (employeeModel != null)
                {
                    userId = employeeModel.EmployeeId;
                    userData = employeeModel;
                }
            }
            else if (user.AccountType == ((int)Domain.Enum.AccountType.Client))
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
                {
                    var response = new HttpResponseDto<object>
                    {
                        StatusCode = 0,
                        Message = "No account for this user",
                        StatusDescreption = "Failed"
                    };
                    return BadRequest(response);

                }

                if (account.IsActive == false)
                {
                    var response = new HttpResponseDto<object>
                    {
                        StatusCode = 0,
                        Message = "this account is suspend",
                        StatusDescreption = "Failed"
                    };
                    return BadRequest(response);
                }
                
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                {
                    var response = new HttpResponseDto<object>
                    {
                        StatusCode = 0,
                        Message = "No roles for this user",
                        StatusDescreption = "Failed"
                    };
                    return BadRequest(response);
                }

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, user.Password);
                if (account.AccountPassword != password)
                {
                    var response = new HttpResponseDto<object>
                    {
                        StatusCode = 0,
                        Message = "Invalid username or password",
                        StatusDescreption = "Failed"
                    };
                    return BadRequest(response);
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
            HttpResponseDto<ClientRegisterResponseDto> response;
            ClientRegisterResponseDto clientRegisterResponseDto;
            try
            {
                if (!ModelState.IsValid)
                {
                    response = HttpResponseDtoFactory<ClientRegisterResponseDto>
                        .CreateErrorResponseMessage("Empty username or password", App.Global.Eumns.ResponseStatusCode.EmptyData);
                    return BadRequest(response);
                }

                var client = await clientService.GetByPhone(clientRegisterDto.Phone);
                if (client != null)
                {
                    var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                    if(account == null)
                    {
                        account = await registerService.RegisterClientAccount(client, clientRegisterDto);
                    }
                    clientRegisterResponseDto = new ClientRegisterResponseDto
                    {
                        Client = client,
                        OtpCode = account.MobileOtpCode,
                        OTPExpireTime = account.LastOtpCreationTime.Value,
                        SecurityCode = account.AccountSecurityCode
                    };
                    response = HttpResponseDtoFactory<ClientRegisterResponseDto>
                        .CreateSuccessResponse(clientRegisterResponseDto, App.Global.Eumns.ResponseStatusCode.AlreadyExist, "This phone is already token");
                    return Ok(response);
                }
               
                clientRegisterResponseDto = await registerService.RegisterClient(clientRegisterDto);
                if (clientRegisterResponseDto == null)
                {
                    return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
                }

                response = HttpResponseDtoFactory<ClientRegisterResponseDto>.CreateSuccessResponse(clientRegisterResponseDto, App.Global.Eumns.ResponseStatusCode.ClientRegisterdSuccessfully);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientRegisterDto.Phone, $"Your confirmation OTP code is {clientRegisterResponseDto.OtpCode}");
                return Created($"https://{HttpContext.Request.Host}/api/client/get/{clientRegisterResponseDto.Client.ClientId}", response);
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmOTPCode")]
        public async Task<ActionResult<HttpResponseDto<ClientT>>> ConfirmOTPCode(int? clientId, string phone, string otpCode, string signature)
        {
            HttpResponseDto<ClientT> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(otpCode) || string.IsNullOrEmpty(signature))
                {
                    response = HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Empty data", App.Global.Eumns.ResponseStatusCode.EmptyData);
                    return NotFound(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("This client not found", App.Global.Eumns.ResponseStatusCode.NotFound);
                    return NotFound(response);
                }

                var passwordHash = App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + otpCode + account.AccountSecurityCode);
                if (!HttpContext.User.Identity.IsAuthenticated && passwordHash.ToLower() != signature.ToLower())
                {
                    response = HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature);
                    return BadRequest(response);
                }

                if (account.MobileOtpCode != otpCode)
                {
                    response = HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("OTP code is invalid", App.Global.Eumns.ResponseStatusCode.InvalidOTP);
                    return BadRequest(response);
                }

                if (account.LastOtpCreationTime.Value.AddMilliseconds(GeneralSetting.OTPExpireMinutes) > DateTime.Now)
                {
                    response = HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("OTP code is expired", App.Global.Eumns.ResponseStatusCode.OTPExpired);
                    return BadRequest(response);
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
            catch (Exception ex) 
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResendOTPCode")]
        public async Task<ActionResult<HttpResponseDto<OTPCodeDto>>> ResendOTPCode(int? clientId, string phone, string signature)
        {
            HttpResponseDto<OTPCodeDto> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(signature))
                {
                    response = HttpResponseDtoFactory<OTPCodeDto>.CreateErrorResponseMessage("Empty data", App.Global.Eumns.ResponseStatusCode.EmptyData);
                    return NotFound(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = HttpResponseDtoFactory<OTPCodeDto>.CreateErrorResponseMessage("This client not found", App.Global.Eumns.ResponseStatusCode.NotFound);
                    return NotFound(response);
                }

                if (!HttpContext.User.Identity.IsAuthenticated && App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + account.AccountSecurityCode).ToLower() != signature.ToLower())
                {
                    response = HttpResponseDtoFactory<OTPCodeDto>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature);
                    return BadRequest(response);
                }

                account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
                account.LastOtpCreationTime = DateTime.Now;
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", phone, $"Your confirmation OTP code is {account.MobileOtpCode}");
                int affectedRecord = await accountService.Update(account);
                if (affectedRecord < 0)
                {
                    return BadRequest(HttpResponseDtoFactory<OTPCodeDto>.CreateErrorResponse());
                }
                return Ok(HttpResponseDtoFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
                {
                    OTPCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateExceptionResponse(ex));
            }
            
        }

    }
}
