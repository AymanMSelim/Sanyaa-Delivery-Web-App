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
using SanyaaDelivery.Domain;
using App.Global.ExtensionMethods;
using System.Security.Claims;

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
                       Token = tokenService.CreateToken(new AccountT { AccountId = 52, AccountTypeId = 3, AccountReferenceId = "10516", AccountUsername = "01090043513", AccountRoleT = new List<AccountRoleT> { new AccountRoleT {  Role = new RoleT { RoleName = "CustomerApp" } } } }),
                       TokenExpireDate = DateTime.Now.AddDays(30),
                   });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<OpreationResultMessage<object>>> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                var response = OpreationResultMessageFactory<string>.CreateModelNotValidResponse("Empty username or password");
                return Ok(response);
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
                    var response = new OpreationResultMessage<object>
                    {
                        StatusCode = 0,
                        Message = "No account for this user",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);

                }

                if (account.IsActive == false)
                {
                    var response = new OpreationResultMessage<object>
                    {
                        StatusCode = 0,
                        Message = "this account is suspend",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }

                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                {
                    var response = new OpreationResultMessage<object>
                    {
                        StatusCode = 0,
                        Message = "No roles for this user",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, user.Password);
                if (account.AccountPassword != password)
                {
                    var response = new OpreationResultMessage<object>
                    {
                        StatusCode = 0,
                        Message = "Invalid username or password",
                        StatusDescreption = "Failed"
                    };
                    return Ok(response);
                }
                return Ok(
                    new OpreationResultMessage<SystemUserDto>
                    {
                        StatusCode = 1,
                        Message = "Token Generated Sussessfuly",
                        StatusDescreption = "Success",
                        Data = new SystemUserDto
                        {
                            Username = user.Username,
                            Token = "",//tokenService.CreateToken(account.AccountId, account.AccountReferenceId account.AccountUsername, accountRoles),
                            TokenExpireDate = DateTime.Now.AddDays(30),
                            UserData = userData
                        }
                    });
            }
            else
            {
                return Ok(new OpreationResultMessage<object> { StatusCode = 0, Message = "Invalid username or password", StatusDescreption = "Failed" });
            }
        }

        [AllowAnonymous]
        [HttpPost("LoginClient")]
        public async Task<ActionResult<OpreationResultMessage<SystemUserDto>>> LoginClient(ClientLoginDto clientLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("Empty username or password", App.Global.Enums.OpreationResultStatusCode.EmptyData));
                }

                var client = await clientService.GetByPhone(clientLoginDto.Username);
                if (client == null)
                {
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("This client not found", App.Global.Enums.OpreationResultStatusCode.NotFound));
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("No account for this client", App.Global.Enums.OpreationResultStatusCode.NotFound));

                if (account.IsMobileVerfied == false)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponse(new ClientRegisterResponseDto { Client = client, OtpCode = account.MobileOtpCode, SecurityCode = account.AccountSecurityCode, OTPExpireTime = account.LastOtpCreationTime.Value }, App.Global.Enums.OpreationResultStatusCode.MobileVerificationRequired, "This account not verified"));

                if (account.IsActive == false)
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("Account is suspended", App.Global.Enums.OpreationResultStatusCode.AccountSuspended));

                if (account.IsPasswordReseted.HasValue && account.IsPasswordReseted.Value)
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("Reset password required", App.Global.Enums.OpreationResultStatusCode.ResetPasswordRequired));


                if (GeneralSetting.IsEmailVerificationRequired)
                {
                    if (account.IsEmailVerfied == false)
                        return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("Email verification required", App.Global.Enums.OpreationResultStatusCode.EmailVerificationRequired));
                }
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("No roles for this user", App.Global.Enums.OpreationResultStatusCode.NoRoleFound));

                var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, clientLoginDto.Password);
                if (account.AccountPassword != password)
                {
                    return Ok(OpreationResultMessageFactory<SystemUserDto>.CreateErrorResponseMessage("Invalid username or password", App.Global.Enums.OpreationResultStatusCode.InvalidUserOrPassword));
                }
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                    OpreationResultMessageFactory<SystemUserDto>.CreateSuccessResponse(
                         new SystemUserDto
                         {
                             Username = clientLoginDto.Username,
                             Token = token,
                             TokenExpireDate = DateTime.Now.AddDays(30),
                             UserData = client,
                             AccountId = account.AccountId
                         }
                        ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> LoginClientWithOtp(ClientLoginWithOtpDto clientLoginWithOtpDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Empty phone or otp code", App.Global.Enums.OpreationResultStatusCode.EmptyData));
                }

                var client = await clientService.GetByPhone(clientLoginWithOtpDto.Phone);
                if (client == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("This client not found", App.Global.Enums.OpreationResultStatusCode.NotFound));
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("No account for this client", App.Global.Enums.OpreationResultStatusCode.NotFound));

                if (account.IsMobileVerfied == false)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponse(new ClientRegisterResponseDto { Client = client, OtpCode = account.MobileOtpCode, SecurityCode = account.AccountSecurityCode, OTPExpireTime = account.LastOtpCreationTime.Value }, App.Global.Enums.OpreationResultStatusCode.MobileVerificationRequired, "This account not verified"));

                if (account.IsActive == false)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Account is suspended", App.Global.Enums.OpreationResultStatusCode.AccountSuspended));

                if (account.IsPasswordReseted.HasValue && account.IsPasswordReseted.Value)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Reset password required", App.Global.Enums.OpreationResultStatusCode.ResetPasswordRequired));


                if (GeneralSetting.IsEmailVerificationRequired)
                {
                    if (account.IsEmailVerfied == false)
                        return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Email verification required", App.Global.Enums.OpreationResultStatusCode.EmailVerificationRequired));
                }
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                if (accountRoles == null)
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("No roles for this user", App.Global.Enums.OpreationResultStatusCode.NoRoleFound));

                //var password = App.Global.Encreption.Hashing.ComputeHMACSHA512Hash(account.AccountHashSlat, clientLoginDto.Password);
                if (account.MobileOtpCode != clientLoginWithOtpDto.OtpCode)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Invalid phone or otp code", App.Global.Enums.OpreationResultStatusCode.InvalidUserOrPassword));
                }
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                    OpreationResultMessageFactory<SystemUserDto>.CreateSuccessResponse(
                         new SystemUserDto
                         {
                             Username = clientLoginWithOtpDto.Phone,
                             Token = token,
                             TokenExpireDate = DateTime.Now.AddDays(30),
                             UserData = client,
                             AccountId = account.AccountId
                         }
                        ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterClient")]
        public async Task<ActionResult<OpreationResultMessage<ClientRegisterResponseDto>>> RegisterClient(ClientRegisterDto clientRegisterDto)
        {
            OpreationResultMessage<ClientRegisterResponseDto> response;
            ClientRegisterResponseDto clientRegisterResponseDto;
            try
            {
                if (!ModelState.IsValid)
                {
                    response = OpreationResultMessageFactory<ClientRegisterResponseDto>
                        .CreateErrorResponseMessage("Empty username or password", App.Global.Enums.OpreationResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var client = await clientService.GetByPhone(clientRegisterDto.Phone);
                if (client != null)
                {
                    var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                    if (account == null)
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
                    response = OpreationResultMessageFactory<ClientRegisterResponseDto>
                        .CreateSuccessResponse(clientRegisterResponseDto, App.Global.Enums.OpreationResultStatusCode.AlreadyExist, "This phone is already token");
                    return Ok(response);
                }

                clientRegisterResponseDto = await registerService.RegisterClient(clientRegisterDto);
                if (clientRegisterResponseDto == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }

                response = OpreationResultMessageFactory<ClientRegisterResponseDto>.CreateSuccessResponse(clientRegisterResponseDto, App.Global.Enums.OpreationResultStatusCode.ClientRegisterdSuccessfully);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientRegisterDto.Phone, $"Your confirmation OTP code is {clientRegisterResponseDto.OtpCode}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmOTPCode")]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> ConfirmRegisterOTPCode(int? clientId, string phone, string otpCode, string signature)
        {
            OpreationResultMessage<ClientT> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(otpCode) || string.IsNullOrEmpty(signature))
                {
                    response = OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Empty data", App.Global.Enums.OpreationResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("This client not found", App.Global.Enums.OpreationResultStatusCode.NotFound);
                    return Ok(response);
                }

                var passwordHash = App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + otpCode + account.AccountSecurityCode);
                if (!HttpContext.User.Identity.IsAuthenticated && passwordHash.ToLower() != signature.ToLower())
                {
                    response = OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.OpreationResultStatusCode.InvalidSignature);
                    return Ok(response);
                }

                if (account.MobileOtpCode != otpCode)
                {
                    response = OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("OTP code is invalid", App.Global.Enums.OpreationResultStatusCode.InvalidOTP);
                    return Ok(response);
                }

                if (accountService.IsOtpExpired(account))
                {
                    response = OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("OTP code is expired", App.Global.Enums.OpreationResultStatusCode.OTPExpired);
                    return Ok(response);
                }

                int affectedRecord = await accountService.ConfirmRegisterOtp(account);
                if (affectedRecord < 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
                var client = await clientService.GetByPhone(phone);
                var accountRoles = await accountRoleService.GetList(account.AccountId, true);
                account.AccountRoleT = accountRoles;
                string token = tokenService.CreateToken(account);
                _ = tokenService.AddAsync(new TokenT { AccountId = account.AccountId, CreationTime = DateTime.Now, Token = token });
                return Ok(
                  OpreationResultMessageFactory<SystemUserDto>.CreateSuccessResponse(
                       new SystemUserDto
                       {
                           Username = phone,
                           Token = token,
                           TokenExpireDate = DateTime.Now.AddDays(30),
                           UserData = client,
                           AccountId = account.AccountId
                       }
                      ));

                //return Ok(HttpResponseDtoFactory<ClientT>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResendOTPCode")]
        public async Task<ActionResult<OpreationResultMessage<OTPCodeDto>>> ResendOTPCode(int? clientId, string phone, string signature)
        {
            OpreationResultMessage<OTPCodeDto> response;
            try
            {
                if (!clientId.HasValue || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(signature))
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("Empty data", App.Global.Enums.OpreationResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, clientId.ToString());
                if (account == null)
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("This client not found", App.Global.Enums.OpreationResultStatusCode.NotFound);
                    return Ok(response);
                }

                if (!HttpContext.User.Identity.IsAuthenticated && App.Global.Encreption.Hashing.ComputeSha256Hash(clientId + phone + account.AccountSecurityCode).ToLower() != signature.ToLower())
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.OpreationResultStatusCode.InvalidSignature);
                    return Ok(response);
                }

                if (accountService.IsMaxOtpPerDayReached(account))
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("Maximum otp has been reached", App.Global.Enums.OpreationResultStatusCode.MaximumCountReached);
                    return Ok(response);
                }
                account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
                account.LastOtpCreationTime = DateTime.Now;
                int affectedRecord = await accountService.Update(account);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", phone, $"Your confirmation OTP code is {account.MobileOtpCode}");
                if (affectedRecord < 0)
                {
                    return Ok(OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponse());
                }
                return Ok(OpreationResultMessageFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
                {
                    OTPCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [AllowAnonymous]
        [HttpGet("RequestOTPForLogin/{clientPhone}")]
        public async Task<ActionResult<OpreationResultMessage<OTPCodeDto>>> RequestOTPForLogin(string clientPhone)
        {
            OpreationResultMessage<OTPCodeDto> response;
            try
            {
                if (string.IsNullOrEmpty(clientPhone))
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("Empty data", App.Global.Enums.OpreationResultStatusCode.EmptyData);
                    return Ok(response);
                }

                var client = await clientService.GetByPhone(clientPhone);
                if (client.IsNull())
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateNotFoundResponse("This client not found");
                    return Ok(response);
                }
                var account = await accountService.Get(GeneralSetting.CustomerAccountTypeId, client.ClientId.ToString());
                if (account == null)
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = clientPhone });
                }

                if (accountService.IsMaxOtpPerDayReached(account))
                {
                    response = OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponseMessage("Maximum otp has been reached", App.Global.Enums.OpreationResultStatusCode.MaximumCountReached);
                    return Ok(response);
                }
                account.MobileOtpCode = App.Global.Generator.GenerateOTPCode(4);
                account.LastOtpCreationTime = DateTime.Now;
                int affectedRecord = await accountService.Update(account);
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", clientPhone, $"Your confirmation OTP code is {account.MobileOtpCode}");
                if (affectedRecord < 0)
                {
                    return Ok(OpreationResultMessageFactory<OTPCodeDto>.CreateErrorResponse());
                }
                return Ok(OpreationResultMessageFactory<OTPCodeDto>.CreateSuccessResponse(new OTPCodeDto
                {
                    OTPCode = account.MobileOtpCode,
                    OTPExpireTime = account.LastOtpCreationTime.Value.AddMinutes(GeneralSetting.OTPExpireMinutes)
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [AllowAnonymous]
        [HttpGet("IsClientExist/{phoneNumber}")]
        public async Task<ActionResult<OpreationResultMessage<object>>> IsClientExist(string phoneNumber)
        {
            try
            {
                var client = await clientService.GetByPhone(phoneNumber);
                if (client.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateNotFoundResponse());
                }
                var account = await accountService.Get((int)Domain.Enum.AccountType.Client, client.ClientId.ToString());
                if (account.IsNull())
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = phoneNumber });
                }
                return Ok(OpreationResultMessageFactory<object>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.AlreadyExist));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetClientPassword/{phoneNumber}")]
        public async Task<ActionResult<OpreationResultMessage<object>>> ResetClientPassword(string phoneNumber)
        {
            try
            {
                var client = await clientService.GetByPhone(phoneNumber);
                if (client.IsNull())
                {
                    return OpreationResultMessageFactory<object>.CreateNotFoundResponse("This phone number is not registered");
                }

                var account = await accountService.Get((int)Domain.Enum.AccountType.Client, client.ClientId.ToString());
                if (account.IsNull())
                {
                    account = await registerService.RegisterClientAccount(client, new ClientRegisterDto { Phone = phoneNumber });
                }

                if (account.IsNull())
                {
                    return OpreationResultMessageFactory<object>.CreateErrorResponse();
                }

                if (account.AccountUsername != phoneNumber)
                {
                    return OpreationResultMessageFactory<object>.CreateNotFoundResponse("No account find for this phone number");
                }

                if (accountService.IsMaxResetPasswordPerDayReached(account))
                {
                    return OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Maximum password reset has been reached", App.Global.Enums.OpreationResultStatusCode.MaximumCountReached);
                }

                var resetPasswordResult = await accountService.ResetPassword(account);
                if (resetPasswordResult <= 0)
                {
                    return OpreationResultMessageFactory<object>.CreateErrorResponse();
                }
                _ = App.Global.SMS.SMSMisrService.SendSmsAsync("2", phoneNumber, $"Your confirmation OTP code is {account.MobileOtpCode} expired after {GeneralSetting.OTPExpireMinutes} minutes");
                return OpreationResultMessageFactory<object>.CreateSuccessResponse(new { OtpCode = account.MobileOtpCode, AccountId = account.AccountId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmResetPasswordOTP")]
        public async Task<ActionResult<OpreationResultMessage<object>>> ConfirmResetPasswordOTP(int accountId, string otpCode)
        {
            try
            {
                var account = await accountService.Get(accountId);
                if (account.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.OpreationResultStatusCode.AccountSuspended));
                }

                if (account.IsPasswordReseted == false)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Password reset has not been requested", App.Global.Enums.OpreationResultStatusCode.ResetPasswordNotRequested));
                }


                //var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(accountId + account.AccountUsername + otpCode + account.ResetPasswordToken);
                //if(!HttpContext.User.Identity.IsAuthenticated && calculatedSignature.ToLower() != signature.ToLower())
                //{
                //    return Ok(HttpResponseDtoFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Eumns.ResponseStatusCode.InvalidSignature));
                //}

                if (account.MobileOtpCode != otpCode)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("OTP code is invalid", App.Global.Enums.OpreationResultStatusCode.InvalidOTP));
                }

                if (accountService.IsOtpExpired(account))
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("OTP code is expired", App.Global.Enums.OpreationResultStatusCode.OTPExpired));
                }

                int affectedRecord = await accountService.ConfirmResetPasswordOtp(account);
                if (affectedRecord < 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
                return Ok(OpreationResultMessageFactory<object>.CreateSuccessResponse(new { ResetToken = account.ResetPasswordToken }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public async Task<ActionResult<OpreationResultMessage<object>>> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateModelNotValidResponse("Model not valid"));
                }
                var account = await accountService.Get(updatePasswordDto.AccountId);
                if (account.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.OpreationResultStatusCode.AccountSuspended));
                }

                var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(updatePasswordDto.AccountId + account.AccountUsername + account.ResetPasswordToken);
                if (!HttpContext.User.Identity.IsAuthenticated && (string.IsNullOrEmpty(updatePasswordDto.Signature) || calculatedSignature.ToLower() != updatePasswordDto.Signature.ToLower()))
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.OpreationResultStatusCode.InvalidSignature));
                }

                int affectedRecord = await accountService.UpdatePassword(account, updatePasswordDto.Password);
                if (affectedRecord < 0)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponse());
                }
                return Ok(OpreationResultMessageFactory<object>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpGet("RenewToken")]
        public async Task<ActionResult<OpreationResultMessage<object>>> RenewToken(int? accountId = null, string signature = null)
        {
            try
            {
                if (accountId.IsNull() && !HttpContext.User.Identity.IsAuthenticated)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Account id is null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                if(HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    var nameIdentifier = App.Global.JWT.TokenHelper.GetAccountId(identity);
                    if (nameIdentifier != null)
                    {
                        accountId = int.Parse(nameIdentifier);
                    }
                }
                var account = await accountService.Get(accountId.Value);
                if (account.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateNotFoundResponse("This account not found"));
                }

                if (account.IsActive == false)
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Your account is suspended please contact us", App.Global.Enums.OpreationResultStatusCode.AccountSuspended));
                }

                var calculatedSignature = App.Global.Encreption.Hashing.ComputeSha256Hash(accountId + account.AccountUsername + account.AccountSecurityCode);
                if (!HttpContext.User.Identity.IsAuthenticated && (string.IsNullOrEmpty(signature) || calculatedSignature.ToLower() != signature.ToLower()))
                {
                    return Ok(OpreationResultMessageFactory<object>.CreateErrorResponseMessage("Invalid data siganture", App.Global.Enums.OpreationResultStatusCode.InvalidSignature));
                }
                account.AccountRoleT = await accountRoleService.GetList(accountId.Value, true);
                return Ok(
                       new
                       {
                           AccountId = accountId,
                           Token = tokenService.CreateToken(account),
                           TokenExpireDate = DateTime.Now.AddDays(30),
                       });
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse(ex));
            }
        }

    }
}
